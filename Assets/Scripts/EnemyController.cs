using UnityEngine;
using System.Collections;
//using System;

public class EnemyController : MonoBehaviour {

    public GameObject PlayerHead;
    public Camera GhoulEyes;
    public GameObject[] Rooms;
    public float GazeOnPlayerTime = 1.2f;

    float _gazeOnPlayerTimer = 0;
    Vector3 _targetLocation;
    public bool _hasTagetLocation = false, _survey = false, _surveyingRunning = false, _playerSpottet = false;
    NavMeshAgent _enemy;
    Animator _aniController;
    Rigidbody _rigidBody;
    Renderer _playerHeadRenderer;

    Vector3 _positionLastFrame;
	// Use this for initialization
	void Start () {
        _enemy = this.gameObject.GetComponent<NavMeshAgent>();
        _aniController = this.gameObject.GetComponent<Animator>();
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _positionLastFrame = this.gameObject.transform.position;
        _playerHeadRenderer = PlayerHead.GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        
        //makes the animation of walking activate and deactivate
        if(!_aniController.GetBool("Walking") && this.gameObject.transform.position != _positionLastFrame)
        {
            _aniController.SetBool("Walking", true);
        }
        if (this.gameObject.transform.position == _positionLastFrame)
        {
            _aniController.SetBool("Walking", false);
        }

        _positionLastFrame = this.gameObject.transform.position;



        if (_playerHeadRenderer.isVisible)
        {
            _playerSpottet = true;
            _gazeOnPlayerTimer += Time.deltaTime;
        }
        else
        {
            _playerSpottet = false;
            _gazeOnPlayerTimer = 0;
        }

        if (_surveyingRunning)
        {
            return;
        }

        if (_survey)
        {
            StartCoroutine(Survey());
            return;
        }

        if (_enemy.remainingDistance == 0 && _hasTagetLocation)
        {
            _survey = true;
            _hasTagetLocation = false;
            return;
        }
        if (!_hasTagetLocation && !_surveyingRunning)
        {
            PickDestination();
        }

        /*if (_targetLocation == Vector3.zero)
        {
            _targetLocation = Entrance.transform.position;
            _enemy.SetDestination(_targetLocation);
            _aniController.SetBool("Walking", true);
            return;
        }
        if(_enemy.remainingDistance == 0)
        {
            _targetLocation = ComputerRoom.transform.position;
            _enemy.SetDestination(_targetLocation);
        }*/
	}

    private void PickDestination()
    {
        int pos = Random.Range(0, Rooms.Length);
        _targetLocation = Rooms[pos].transform.position;
        _enemy.SetDestination(_targetLocation);
        _hasTagetLocation = true;
    }

    IEnumerator Survey()
    {
        _surveyingRunning = true;
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(1.5f);
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(1.5f);
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(2);
        _survey = false;
        _surveyingRunning = false;
        yield return null;
    }

    IEnumerator Chase()
    {
        _surveyingRunning = true;
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(1.5f);
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(1.5f);
        _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
        yield return new WaitForSeconds(2);
        _survey = false;
        _surveyingRunning = false;
        yield return null;
    }
}
