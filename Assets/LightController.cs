using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

    GameObject[] _lightsources;
    public float LightRange = 100f;
    public float LightIntencity = 100f;
    public float LightManipulationAmount = 100;

    // Use this for initialization
    void Start () {
        _lightsources = GameObject.FindGameObjectsWithTag("LightSource");
        
	}
	
	// Update is called once per frame
	void Update () {
       for(int i = 0; i< _lightsources.Length; i++)
        {
            if (_lightsources[i].GetComponent<StartLightSettings>())
            {
                //Debug.Log("light found");
                _lightsources[i].GetComponent<StartLightSettings>().AdjustRange(LightRange);

                if (Random.Range(-1, 1) < 0 && _lightsources[i].GetComponent<Light>().intensity>0.3f)
                {
                    _lightsources[i].GetComponent<StartLightSettings>().AdjustIntencity(100 - LightManipulationAmount * Time.deltaTime);
                    _lightsources[i].GetComponent<StartLightSettings>().AdjustRange(100 - LightManipulationAmount * Time.deltaTime);
                }
                else if (_lightsources[i].GetComponent<Light>().intensity < 1.3f)
                {
                    _lightsources[i].GetComponent<StartLightSettings>().AdjustIntencity(100 + LightManipulationAmount * Time.deltaTime);
                    _lightsources[i].GetComponent<StartLightSettings>().AdjustRange(100 + LightManipulationAmount * Time.deltaTime);
                }
            }
        }
	}
}
