using UnityEngine;
using System.Collections;

public class ChairMovement : MonoBehaviour {

    public bool LookedAt = false;
    public float animationSpeed = 0.3f;
    bool _move = false;
    float timeLookedAt, timeToTrigger, animationDistance, count = 0;
    Vector3 startPos;
    public GameObject lamp;
    GameObject lm;
    AudioControl _ac;

	// Use this for initialization
	void Start () {
        timeToTrigger = 1f;
        startPos = transform.position;
        lm = GameObject.FindGameObjectWithTag("LevelManager");
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }
	
	// Update is called once per frame
	void Update () {
        if(_move && count == 0)
        {
            lm.GetComponent<LightController>().OneLampBlink(lamp, 3f);
            _ac.StartDragging();
            count = 1;
        }
        if (_move && animationDistance < 1)
        {
            animationDistance += Time.deltaTime * animationSpeed;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(-0.5f,0,0), animationDistance);
            
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
