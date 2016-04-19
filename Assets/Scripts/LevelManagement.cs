﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {

    public GameObject MonsterDoorOpen, MonsterDoorClosed;

    public GameObject PressEToOpenDoor;
    public GameObject PressEToPickUpKey;

    public GameObject KeyToPickUp;

    public Text objectiveText;


    public int RequiredKeyEvents = 3;
    public int FiredEvents;
    public int FiredKeyEvents;

    private bool _key;
    private AudioControl _ac;
    private EnemyController _enemy;
    private LightController _LightControl;
    private EyeRayCaster _triggerZoneManagement;

    public static bool FirstTimeOpening = true,
                 SecoundTimeOpening = false;

    // Use this for initialization
    void Start() {
        objectiveText.text = "Find the main entrance";
        RenderSettings.ambientIntensity = 0.2f;
        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();
        _enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        _LightControl = this.gameObject.GetComponent<LightController>();
        _triggerZoneManagement = this.gameObject.GetComponent<EyeRayCaster>();

        _key = false;
        FiredEvents = 0;
        FiredKeyEvents = 0;
    }

    // Update is called once per frame
    void Update() {

        if (FiredKeyEvents >= RequiredKeyEvents && !KeyToPickUp.gameObject.activeSelf && !_key)
        {
            KeyToPickUp.SetActive(true);
        }

        //Handle Key pick up
        if (KeyToPickUp.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("e"))
            {
                //Open door
                _key = true;
                PressEToPickUpKey.SetActive(false);
                KeyToPickUp.SetActive(false);

                //Next time will be secound time
                SecoundTimeOpening = true;

            }
        }
        //Handle opening main entrance
        if (PressEToOpenDoor.gameObject.activeSelf)
        {
            //First try
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

                //Enable TriggerZones
                _triggerZoneManagement.StartTriggerZones = true;

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
            //secound try
            if (Input.GetKeyDown("e") && SecoundTimeOpening && _key && PressEToOpenDoor.gameObject.activeSelf)
            {
                //Open door
                Application.Quit();

                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }


	}


}
