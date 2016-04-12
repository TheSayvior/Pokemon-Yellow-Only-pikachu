using UnityEngine;
using System.Collections;

public class EyeRayCaster : MonoBehaviour {

    //float timeLookedAt = 0f;
    private GameObject _ActiveObject;
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
                Debug.Log("rammer nu");
                if (hit.transform.gameObject.GetComponent<ChairMovement>())
                {
                    _ActiveObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<ChairMovement>().LookedAt = true;
                    return;
                }
                if (hit.transform.gameObject.GetComponent<PianoTrigger>())
                {
                    _ActiveObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<PianoTrigger>().LookedAt = true;
                    return;
                }
                if (hit.transform.gameObject.GetComponent<ToiletTrigger>())
                {
                    _ActiveObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<ToiletTrigger>().LookedAt = true;
                    return;
                }
                if (hit.transform.gameObject.GetComponent<MirrorTrigger>())
                {
                    _ActiveObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<MirrorTrigger>().LookedAt = true;
                    return;
                }
                if (hit.transform.gameObject.GetComponent<TVTrigger>())
                {
                    _ActiveObject = hit.transform.gameObject;
                    hit.transform.gameObject.GetComponent<TVTrigger>().LookedAt = true;
                    return;
                }
            }
            if (_ActiveObject != null)
            {
                _ActiveObject.GetComponent<ChairMovement>().LookedAt = false;
                _ActiveObject.GetComponent<PianoTrigger>().LookedAt = false;
                _ActiveObject.GetComponent<MirrorTrigger>().LookedAt = false;
                _ActiveObject.GetComponent<ToiletTrigger>().LookedAt = false;
                _ActiveObject.GetComponent<TVTrigger>().LookedAt = false;
            }
            _ActiveObject = null;
        }
    }
}
