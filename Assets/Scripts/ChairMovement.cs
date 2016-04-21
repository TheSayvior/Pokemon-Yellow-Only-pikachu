using UnityEngine;
using System.Collections;

public class ChairMovement : MonoBehaviour {

    public bool LookedAt = false;
    public float animationSpeed = 0.3f;
    public bool KeyTrigger;
    bool _move = false;
    float timeLookedAt, timeToTrigger, animationDistance;
    Vector3 startPos;
    public GameObject lamp;
    GameObject lm;
    AudioControl _ac;
    LevelManagement lmScript;

    public GameObject TriggerCollider;
    private CollisionController _cc;
    private Renderer _renderer;

    private bool EventFired = false;
    private bool CollisionHappend = false;

    // Use this for initialization
    void Start () {
        timeToTrigger = 0.5f;
        startPos = transform.position;
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
	void Update ()
    {
        if (lmScript.MonsterDoorClosed.activeSelf)
        {
            return;
        }
        if (lmScript.TriggerByEyesight)
        {
            TriggerChairMovementByEyesight();
        }
        if (lmScript.TriggerByCollider)
        {
            TriggerChairMovementByCollider();
        }
        if (lmScript.TriggerBySeen)
        {
            Debug.Log(_renderer.isVisible);
            TriggerChairMovementBySeen();
        }
    }

    private void TriggerChairMovementByEyesight()
    {
        if (_move && !EventFired)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3f);
            _ac.StartDragging();
            EventFired = true;

            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
        }
        if (_move && animationDistance < 1)
        {
            animationDistance += Time.deltaTime * animationSpeed;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(-0.5f, 0, 0), animationDistance);

            Debug.Log("Flyttes");
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
        _ac.StopDragging();
        timeLookedAt = 0;
    }
    private void TriggerChairMovementByCollider()
    {
        if ((_cc.Collision || CollisionHappend) && !EventFired)
        {
            _move = true;
            CollisionHappend = true;
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3f);
            _ac.StartDragging();
            EventFired = true;

            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
        }
        if (_move && animationDistance < 1)
        {
            animationDistance += Time.deltaTime * animationSpeed;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(-0.5f, 0, 0), animationDistance);

            Debug.Log("Flyttes");
            return;
        }
        _ac.StopDragging();
    }
    private void TriggerChairMovementBySeen()
    {
        if (_move && !EventFired && !_renderer.isVisible)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3f);
            _ac.StartDragging();
            EventFired = true;

            lmScript.FiredEvents++;
            if (KeyTrigger)
            {
                lmScript.FiredKeyEvents++;
            }
        }
        if (_move && animationDistance < 1 && EventFired)
        {
            animationDistance += Time.deltaTime * animationSpeed;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(-0.5f, 0, 0), animationDistance);

            Debug.Log("Flyttes");
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
        _ac.StopDragging();
        timeLookedAt = 0;
    }
}
