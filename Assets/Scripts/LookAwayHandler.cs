using UnityEngine;
using System.Collections;

public class LookAwayHandler : MonoBehaviour {

    private GameObject _GazeIndicator;

    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
    }
	
	// Update is called once per frame
	void Update () {
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().LookingAtScreen())
        {
            Debug.Log("You are looking, Creep");
        } else
        {
            Debug.Log("You are not Looking at the screen");
        }
    }
}
