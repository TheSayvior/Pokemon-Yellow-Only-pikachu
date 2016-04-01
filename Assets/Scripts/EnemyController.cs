using UnityEngine;
using System.Collections;
//using System;

public class EnemyController : MonoBehaviour {

    public GameObject Player;
    public Camera GhoulEyes;
    public GameObject[] Rooms;
    public float GazeOnPlayerTime = 1.2f;
    public float fieldOfViewAngle = 110f;

    public bool Hunting = false;

    float _gazeOnPlayerTimer = 0;
    Vector3 _targetLocation, _lastPlayerSighting;
    public bool _hasTagetLocation = false, _survey = false, _surveyingRunning = false, _playerSpottet = false;

    private bool _playerInSight = false;
    NavMeshAgent _enemy;
    Animator _aniController;
    Rigidbody _rigidBody;
    Renderer _playerHeadRenderer;
    SphereCollider _eyeSightRange;

    Vector3 _positionLastFrame;
	// Use this for initialization
	void Start () {
        _enemy = this.gameObject.GetComponent<NavMeshAgent>();
        _aniController = this.gameObject.GetComponent<Animator>();
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _positionLastFrame = this.gameObject.transform.position;
        _eyeSightRange = GetComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!Hunting)
        {
            return;
        }
      
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

        if (_playerInSight)
        {
            _enemy.SetDestination(_lastPlayerSighting);
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
        while (!_playerInSight)
        {
            _surveyingRunning = true;
            _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
            yield return new WaitForSeconds(1.5f);
            _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
            yield return new WaitForSeconds(1.5f);
            _enemy.SetDestination(_targetLocation + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
            yield return new WaitForSeconds(2);
        }
        _survey = false;
        _surveyingRunning = false;
        yield return null;
    }

    IEnumerator Chase()
    {
        yield return null;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PlayerInRange");
            _playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // If the angle between forward and where the player is, is less than half the angle of view...
            if (angle < fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;

                // ... and if a raycast towards the player hits something...
                if (Physics.Raycast(this.transform.position, direction.normalized, out hit, _eyeSightRange.radius))
                {
                    Debug.Log(hit.collider.name);
                    // ... and if the raycast hits the player...
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        // ... the player is in sight.
                        Debug.DrawRay(transform.position, direction.normalized, Color.green);
                        _playerInSight = true;
                        Debug.Log(_playerInSight);

                        // Set the last global sighting is the players current position.
                        _lastPlayerSighting = Player.transform.position;

                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        // If the player leaves the trigger zone...
        if (other.gameObject.tag == "Player")
            // ... the player is not in sight.
            _playerInSight = false;
    }
}
