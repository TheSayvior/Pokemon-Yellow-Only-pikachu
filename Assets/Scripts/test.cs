using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public Color endColor;
    public Color startColor;

    Color current;

	// Use this for initialization
	void Start () {
        current = GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void RunEnum()
    {
        if(current != endColor)
        {
            StartCoroutine(ChangeColor(endColor));
            return;
        }
        if (current != startColor)
        {
            StartCoroutine(ChangeColor(startColor));
        }
    }

    void OnMouseDown()
    {
        //GetComponent<Renderer>().material.color = Color.black;
    }

    IEnumerator ChangeColor( Color col)
    {

        GetComponent<Renderer>().material.color = col;
        current = col;
        Debug.Log("changed color");
        return null;
    }
}
