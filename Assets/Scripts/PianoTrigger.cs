using UnityEngine;
using System.Collections;

public class PianoTrigger : MonoBehaviour {
    public bool LookedAt;
    public bool KeyTrigger;
    private float timeLookedAt;
    private float timeToTrigger;
    bool _move = false;

    GameObject lm;
    AudioControl _ac;

    public GameObject lamp, head, model, blackscreen;
    private float count = 0;

    // Use this for initialization
    void Start () {
        timeToTrigger = 1f;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if (_move && count == 0)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3); //make light blink
            StartCoroutine(PianoSequence());
            return;
        }
        if (LookedAt)
        {
            timeLookedAt += Time.deltaTime;
            if (timeLookedAt >= timeToTrigger)
            {
                _move = true;
                Debug.Log("HAR KIGGET i OVER "+timeToTrigger+" SEKUNDER NU");
            }
            return;
        }
        //stop piano tune if not allready done

        timeLookedAt = 0;
    }

    IEnumerator PianoSequence()
    {
        count = 1;
        _ac.StartScare(); //play scare sound
        blackscreen.SetActive(true);//turn off camera for a bit
        head.SetActive(true); //setactive true head
        model.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        blackscreen.SetActive(false);
        _ac.StartPiano();//start play a piano tune
        yield return new WaitForSeconds(1);
        head.SetActive(false); //setactive false head
        model.SetActive(false);

        lm.GetComponent<LevelManagement>().FiredEvents++;
        if (KeyTrigger)
        {
            lm.GetComponent<LevelManagement>().FiredKeyEvents++;
        }
    }
}
