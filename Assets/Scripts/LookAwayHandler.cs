using UnityEngine;
using System.Collections;
using System;

public class LookAwayHandler : MonoBehaviour {

    private GameObject _GazeIndicator;
    private bool blinkTimer = false;
    private float blinkTimeMiliSecounds = 300f;

    private String FileName1 = "GameLookingDataX";
    private String FileName2 = "GameLookingDataY";

    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
        System.IO.File.Create(Application.dataPath + "/Data/Looking" + FileName1 + ".txt");
        System.IO.File.Create(Application.dataPath + "/Data/Looking" + FileName2 + ".txt");
    }
	
	// Update is called once per frame
	void Update () {
        //Checks if we are looking at the screen
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().LookingAtScreen())
        {
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName1 + ".txt", Time.time + "\n");
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName2 + ".txt", "0" +  "\n");
        } else
        {
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName1 + ".txt", Time.time + "\n");
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName2 + ".txt", "1" + "\n");
        }
        //Did We blink?
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().Blinking() && !blinkTimer)
        {
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName1 + ".txt", Time.time + "\n");
            System.IO.File.AppendAllText(Application.dataPath + "/Data/Looking" + FileName2 + ".txt", "2" + "\n");
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
