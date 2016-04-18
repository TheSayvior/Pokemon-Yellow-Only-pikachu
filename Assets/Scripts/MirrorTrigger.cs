using UnityEngine;
using System.Collections;

public class MirrorTrigger : MonoBehaviour {
    public bool LookedAt;
    public bool KeyTrigger;
    private float timeLookedAt;
    private float timeToTrigger;
    bool _move = false;

    GameObject lm;
    AudioControl _ac;

    public GameObject lamp, head;
    private float count = 0;

    // Use this for initialization
    void Start()
    {
        timeToTrigger = 0.8f;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_move && count == 0)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3); //make light blink
            StartCoroutine(MirrorSequence());
            return;
        }
        if (LookedAt)
        {
            timeLookedAt += Time.deltaTime;
            if (timeLookedAt >= timeToTrigger)
            {
                _move = true;
                Debug.Log("HAR KIGGET i OVER " + timeToTrigger + " SEKUNDER NU");
            }
            return;
        }
        timeLookedAt = 0;
    }

    IEnumerator MirrorSequence()
    {
        count = 1;
        head.SetActive(true); // head appear at the towels
        _ac.PlayScaryVoice(); // play a scream
        yield return new WaitForSeconds(1.5f);
        head.SetActive(false); // head disappear

        lm.GetComponent<LevelManagement>().FiredEvents++;
        if (KeyTrigger)
        {
            lm.GetComponent<LevelManagement>().FiredKeyEvents++;
        }
    }
}
