using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {

    public GameObject MonsterDoorOpen, MonsterDoorClosed;

    public GameObject PressEToOpenDoor;
    public Text objectiveText;

    private AudioControl _ac;
    private EnemyController _enemy;
    private LightController _LightControl;

    public static bool FirstTimeOpening = true,
                 SecoundTimeOpening = false;
                   
    // Use this for initialization
    void Start () {
        objectiveText.text = "Find the main entrance";
        RenderSettings.ambientIntensity = 0.2f;
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
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
                // change objective
                objectiveText.text = "Find the hidden key";

                //Remove Text
                PressEToOpenDoor.SetActive(false);

                //We have now tried opening once
                FirstTimeOpening = false;

                //Mess with the lights
                StartCoroutine(_LightControl.FlashAllLightForSecounds(5.0f));
                

                //Open monster door
                MonsterDoorOpen.SetActive(true);
                _ac.StopStartMusic();
                _ac.StartScaryMusic();
                _ac.StartMetalDoor();
                _ac.PlayFlickering();
                MonsterDoorClosed.SetActive(false);

                RenderSettings.ambientIntensity = 0.01f;

                //Activate Monster
                _enemy.Hunting = true;

            }
        }


	}


}
