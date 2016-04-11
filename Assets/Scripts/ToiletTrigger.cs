using UnityEngine;
using System.Collections;

public class ToiletTrigger : MonoBehaviour {
    public bool LookedAt;
    private float timeLookedAt;
    private float timeToTrigger;
    bool _move = false;

    GameObject lm;
    AudioControl _ac;

    public GameObject lamp;
    private float count = 0;

    // Use this for initialization
    void Start()
    {
        timeToTrigger = 0.5f;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_move && count == 0)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 4); //make light blink
            _ac.StartFlush();// make toilet flush sound
            count = 1;
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
}
