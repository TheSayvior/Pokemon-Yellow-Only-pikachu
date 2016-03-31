using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

    GameObject[] _lightsources;
    public float LightRange = 100f;
    public float LightIntencity = 100f;
    float LightManipulationAmount = 200;

    // Use this for initialization
    void Start () {
        _lightsources = GameObject.FindGameObjectsWithTag("LightSource");
        
	}
	
	// Update is called once per frame
	/*void Update () {
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
	}*/

    public IEnumerator flashLightForSecounds(float time)
    {
        float _startTime = Time.time;

        while (_startTime + time > Time.time)
        {          
            for (int i = 0; i < _lightsources.Length; i++)
            {
                if (_lightsources[i].GetComponent<StartLightSettings>())
                {
                    //Debug.Log("light found");
                    _lightsources[i].GetComponent<StartLightSettings>().AdjustRange(LightRange);

                    if (Random.Range(-1, 1) < 0 && _lightsources[i].GetComponent<Light>().intensity > 0.3f)
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
            yield return null;            
        }
        for (int i = 0; i < _lightsources.Length; i++)
        {
            if (_lightsources[i].GetComponent<StartLightSettings>())
            {
                _lightsources[i].GetComponent<StartLightSettings>().ResetIntencity();
                _lightsources[i].GetComponent<StartLightSettings>().ResetRange();
            }
        }
        yield return null;
    }
}
