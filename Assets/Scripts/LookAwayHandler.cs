using UnityEngine;
using System.Collections;

public class LookAwayHandler : MonoBehaviour {

    private GameObject _GazeIndicator;
    private bool blinkTimer = false;
    private float blinkTimeMiliSecounds = 300f;

    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
    }
	
	// Update is called once per frame
	void Update () {
        //Checks if we are looking at the screen
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().LookingAtScreen())
        {
            Debug.Log("You are looking, Creep");
        } else
        {
            Debug.Log("You are not Looking at the screen");
        }
        //Did We blink?
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().Blinking() && !blinkTimer)
        {
            Debug.Log("You blinked");
            StartCoroutine(waitForNextBlink());
        }
    }

    IEnumerator waitForNextBlink()
    {
        blinkTimer = true;
        yield return new WaitForSeconds(blinkTimeMiliSecounds/1000);
        blinkTimer = false;
        yield return null;
    }
}
