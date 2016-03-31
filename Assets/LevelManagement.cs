using UnityEngine;
using System.Collections;

public class LevelManagement : MonoBehaviour {

    public GameObject MonsterDoorOpen, MonsterDoorClosed;

    public GameObject PressEToOpenDoor;

    private EnemyController _enemy;
    private LightController _LightControl;

    public static bool FirstTimeOpening = true,
                 SecoundTimeOpening = false;
                   
    // Use this for initialization
    void Start () {
        RenderSettings.ambientIntensity = 0.2f;

        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        _LightControl = this.gameObject.GetComponent<LightController>();
    }
	
	// Update is called once per frame
	void Update () {
        //Handle opening main entrance
        if (PressEToOpenDoor.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("e") && SecoundTimeOpening)
            {
                //Open door
               

            }

            if (Input.GetKeyDown("e") && FirstTimeOpening)
            {
                //Remove Text
                PressEToOpenDoor.SetActive(false);

                //We have now tried opening once
                FirstTimeOpening = false;

                //Mess with the lights
                StartCoroutine(_LightControl.flashLightForSecounds(5.0f));
                

                //Open monter door
                MonsterDoorOpen.SetActive(true);
                MonsterDoorClosed.SetActive(false);

                RenderSettings.ambientIntensity = 0.03f;

                //Activate Monster
                _enemy.Hunting = true;

                //Play MonsterDoorOpenSound
                //Play door closed sound

            }
        }


	}


}
