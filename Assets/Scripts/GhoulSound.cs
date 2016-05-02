using UnityEngine;
using System.Collections;
using System;

public class GhoulSound : MonoBehaviour
{
    AudioControl _ac;

    public bool StartGrowling = false;
    public bool StartBreathing = true;
    public bool Growling = false;
    public bool breathing = false;

    void Start()
    {
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            if (!Growling)
            {
                StartCoroutine(Growl());
            }
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        StartGrowling = true;
    //        StartBreathing = false;
    //        Debug.Log("Enter");
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Exit");
    //        StartGrowling = false;
    //        StartBreathing = true;
    //    }
    //}

    void Update()
    {
        //if (StartGrowling && !Growling)
        //{
        //    StartCoroutine(Growl());
        //}
        if (!breathing && !Growling)
        {
            StartCoroutine(Breath());
        }
    }

    private IEnumerator Growl()
    {
        Growling = true;
        _ac.PlayGhoulGrowl();
        yield return new WaitForSeconds(8);
        Growling = false;
        yield return null;
    }

    private IEnumerator Breath()
    {
        breathing = true;
        _ac.PlayGhoulBreath();
        yield return new WaitForSeconds(8);
        breathing = false;
        yield return null;
    }
}
