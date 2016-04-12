using UnityEngine;
using System.Collections;
using System;

public class GhoulSound : MonoBehaviour
{
    public AudioControl AudioController;

    public bool StartGrowling = false;
    public bool StartBreathing = true;
    public bool Growling = false;
    public bool breathing = false;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Im not crashed");
        if (other.gameObject.tag == "Player")
        {
            StartGrowling = true;
            StartBreathing = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Im not crashed");
        if (other.gameObject.tag == "Player")
        {
            StartGrowling = false;
            StartBreathing = true;
        }
    }

    void Update()
    {
        if (StartGrowling && !Growling)
        {
            StartCoroutine(Growl());
        }
        if (StartBreathing && !breathing)
        {
            StartCoroutine(Breath());
        }
    }

    private IEnumerator Growl()
    {
        Growling = true;
        AudioController.PlayGhoulGrowl();
        Debug.Log("Played Growl");
        yield return new WaitForSeconds(8);
        Growling = false;
        yield return null;
    }

    private IEnumerator Breath()
    {
        breathing = true;
        AudioController.PlayGhoulBreath();
        Debug.Log("Played Breath");
        yield return new WaitForSeconds(8);
        breathing = false;
        yield return null;
    }
}
