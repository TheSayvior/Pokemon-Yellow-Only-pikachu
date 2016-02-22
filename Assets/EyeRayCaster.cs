using UnityEngine;
using System.Collections;

public class EyeRayCaster : MonoBehaviour {

    private GameObject _GazeIndicator;
    // Use this for initialization
    void Start () {
        _GazeIndicator = GameObject.FindGameObjectWithTag("gazeIndicator");
    }
	
	// Update is called once per frame
	void Update () {

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, _GazeIndicator.transform.position - Camera.main.transform.position, out hit, 100))
        {
            if(hit.transform.tag == "LookAtObject")
            {
                Debug.Log("HIT");
                hit.transform.gameObject.GetComponent<test>().RunEnum();
            }
        }
    }
}
