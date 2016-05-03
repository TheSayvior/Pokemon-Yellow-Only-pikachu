using UnityEngine;
using System.Collections;
using System;

public class blackScream : MonoBehaviour {

    AudioControl _ac;
    public GameObject black;
    string FileName1 = "TriggerEventLogX";
    string FileName2 = "TriggerEventLogY";

    bool running = false;

    // Use this for initialization
    void Start () {
        System.IO.FileStream x = System.IO.File.Create(Application.dataPath + "/Data/Triggers/" + FileName1 + ".txt");
        System.IO.FileStream y = System.IO.File.Create(Application.dataPath + "/Data/Triggers/" + FileName2 + ".txt");
        x.Close();
        y.Close();

        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
        

        
    }
	
	// Update is called once per frame
	void Update () {
        if (!running)
        {
            StartCoroutine(Scare());
        }
	}
    
    IEnumerator Scare()
    {
        running = true;
        yield return new WaitForSeconds(4);
        yield return new WaitForSeconds(UnityEngine.Random.Range(4, 12));
        _ac.ScreamerStart();
        System.IO.File.AppendAllText(Application.dataPath + "/Data/Triggers/" + FileName1 + ".txt", Time.time.ToString("F2") + Environment.NewLine);
        System.IO.File.AppendAllText(Application.dataPath + "/Data/Triggers/" + FileName2 + ".txt", "45" + Environment.NewLine);
        yield return new WaitForSeconds(0.5f);
        _ac.ScreamerStop();
        running = false;
    }
}
