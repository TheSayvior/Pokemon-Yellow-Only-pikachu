using UnityEngine;
using System.Collections;
using System;

public class LookAwayHandler : MonoBehaviour {

    private GameObject _GazeIndicator;
    private bool blinkTimer = false;
    private float blinkTimeMiliSecounds = 300f;

    public String FileName = "GameLookingData";

    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
        System.IO.File.Delete("/Users/Rasmus Jensen/Documents/DTU_Master_Thesis/Assets/Data/" + FileName + ".txt");
    }
	
	// Update is called once per frame
	void Update () {
        //Checks if we are looking at the screen
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().LookingAtScreen())
        {
            System.IO.File.AppendAllText("/Users/Rasmus Jensen/Documents/DTU_Master_Thesis/Assets/Data/" + FileName + ".txt", "\n[" + Time.time + ", 0]");
        } else
        {
            System.IO.File.AppendAllText("/Users/Rasmus Jensen/Documents/DTU_Master_Thesis/Assets/Data/" + FileName + ".txt", "\n[" + Time.time + ", 1]");
        }
        //Did We blink?
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().Blinking() && !blinkTimer)
        {
            System.IO.File.AppendAllText("/Users/Rasmus Jensen/Documents/DTU_Master_Thesis/Assets/Data/" + FileName + ".txt", "\n[" + Time.time + ", 2]");
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
