using UnityEngine;
using System.Collections;

public class blackScream : MonoBehaviour {

    AudioControl _ac;
    public GameObject black;


    // Use this for initialization
    void Start () {
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
        StartCoroutine(Scare());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    IEnumerator Scare()
    {
        yield return new WaitForSeconds(Random.Range(5, 15));
        _ac.ScreamerStart();
        yield return new WaitForSeconds(0.5f);
        _ac.ScreamerStop();
    }
}
