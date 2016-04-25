using UnityEngine;
using System.Collections;

public class ToiletTrigger : MonoBehaviour {
    public bool LookedAt;
    public bool KeyTrigger;
    private float timeLookedAt;
    private float timeToTrigger;
    bool _move = false;

    GameObject lm;
    AudioControl _ac;
    LevelManagement lmScript;

    public GameObject lamp;
    private bool EventFired = false;

    public GameObject TriggerCollider;
    private CollisionController _cc;
    private Renderer _renderer;

    // Use this for initialization
    void Start()
    {
        timeToTrigger = 0.5f;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
        lmScript = lm.GetComponent<LevelManagement>();
        //Needed for collider detection
        if (lmScript.TriggerByCollider)
        {
            _cc = TriggerCollider.GetComponent<CollisionController>();
        }
        //Needed for Has been seen detection
        _renderer = this.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lmScript.MonsterDoorClosed.activeSelf)
        {
            return;
        }
        if (lmScript.TriggerByEyesight)
        {
            TriggerToiletFlushByEyesight();
        }
        if (lmScript.TriggerByCollider)
        {
            TriggerToiletFlushByCollider();
        }
        if (lmScript.TriggerBySeen)
        {
            TriggerToiletFlushBySeen();
        }
    }
    //Done
    private void TriggerToiletFlushByEyesight()
    {
        if (_move && !EventFired)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 4); //make light blink
            _ac.StartFlush();// make toilet flush sound
            EventFired = true;
            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
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
    //Done
    private void TriggerToiletFlushBySeen()
    {
        if (_move && !EventFired && !_renderer.isVisible)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 4); //make light blink
            _ac.StartFlush();// make toilet flush sound
            EventFired = true;
            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
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
    }
    //Done
    private void TriggerToiletFlushByCollider()
    {
        if (_cc.Collision && !EventFired)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 4); //make light blink
            _ac.StartFlush();// make toilet flush sound
            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
            EventFired = true;
            return;
        }
    }
}
