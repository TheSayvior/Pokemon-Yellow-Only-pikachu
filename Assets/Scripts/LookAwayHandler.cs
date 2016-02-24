using UnityEngine;
using System.Collections;

public class LookAwayHandler : MonoBehaviour {

    private GameObject _GazeIndicator;
    private float
        _timer = 0,
        _timeToBlink = 100,
        _errorNoBlink = 500;

    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
    }
	
	// Update is called once per frame
	void Update () {
        if (_GazeIndicator.transform.parent.GetComponent<EyeTribeUnityScript>().LookingAtScreen())
        {
            if(_timer > _timeToBlink && _timer < _errorNoBlink)
            {
                Debug.Log("You blinked at " + System.DateTime.Now);
            }
            _timer = 0;
            Debug.Log("You are looking, Creep");
        } else
        {
            _timer += Time.deltaTime * 1000;
        }
    }
}
