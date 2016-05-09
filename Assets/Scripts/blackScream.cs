using UnityEngine;
using System.Collections;
using System;

public class blackScream : MonoBehaviour {

    AudioControl _ac;
    public GameObject black;
    string FileName1 = "TriggerEventLogX";
    string FileName2 = "TriggerEventLogY";

    private double[] _ealiestPupilSizes = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private EyeTribeUnityScript _eyeData;

    private double InitialPupilSize;

    bool running = false;
    bool InitialPupilSizeCalculated = false;

    bool _calm = true;
    bool _scared = false;

    // Use this for initialization
    void Start () {

        InitialPupilSize = 0.0f;

        System.IO.FileStream x = System.IO.File.Create(Application.dataPath + "/Data/Triggers/" + FileName1 + ".txt");
        System.IO.FileStream y = System.IO.File.Create(Application.dataPath + "/Data/Triggers/" + FileName2 + ".txt");

        x.Close();
        y.Close();

        _ac = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioControl>();

        EyeTribeUnityScript _eyeData = FindObjectOfType<EyeTribeUnityScript>();

        //Start data gathering
        StartCoroutine(MeasureInitialPupilSize());

        StartCoroutine(ScareDection());
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

    IEnumerator MeasureInitialPupilSize()
    {
        yield return new WaitForSeconds(0.5f);
        int count = 0;
        while (Time.time < 7)
        {
            if (_eyeData != null)
            {
                InitialPupilSize = InitialPupilSize + ((_eyeData.LeftEye.PupilSize + _eyeData.RightEye.PupilSize) / 2);
                count++;
            }
            yield return null;
        }
        InitialPupilSize = InitialPupilSize / count;
        InitialPupilSizeCalculated = true;
    }

    IEnumerator ScareDection()
    {
        int count1 = 0;
        double sum = 0.0f;
        double average = 0;
        while (InitialPupilSizeCalculated)
        {
            _ealiestPupilSizes[count1 % 10] = ((_eyeData.LeftEye.PupilSize + _eyeData.RightEye.PupilSize) / 2);
            count1++;
            for(int i = 0; i<_ealiestPupilSizes.Length; i++)
            {
                sum += _ealiestPupilSizes[i];
            }
            average = sum / _ealiestPupilSizes.Length;
            if (average > 3 + InitialPupilSize)
            {
                System.IO.File.AppendAllText(Application.dataPath + "/Data/Triggers/" + FileName1 + ".txt", Time.time.ToString("F2") + Environment.NewLine);
                System.IO.File.AppendAllText(Application.dataPath + "/Data/Triggers/" + FileName2 + ".txt", "5" + Environment.NewLine);
            }
            yield return null;
        }
        yield return null;
    }
}
