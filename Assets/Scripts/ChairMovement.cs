using UnityEngine;
using System.Collections;

public class ChairMovement : MonoBehaviour {

    public bool LookedAt = false;
    public float animationSpeed = 0.3f;
    bool _move = false;
    float timeLookedAt, timeToTrigger, animationDistance;
    Vector3 startPos;

	// Use this for initialization
	void Start () {
        timeToTrigger = 1f;
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
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
                Debug.Log("HAR KIGGET i OVER 2 SEKUNDER NU");
            }
            return;
        }
        timeLookedAt = 0;
	}
}
