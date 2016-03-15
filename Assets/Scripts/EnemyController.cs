using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject ComputerRoom,Entrance;

    Vector3 _targetLocation;
    NavMeshAgent _enemy;
    Animator _aniController;
    Rigidbody _rigidBody;
	// Use this for initialization
	void Start () {
        _enemy = this.gameObject.GetComponent<NavMeshAgent>();
        _aniController = this.gameObject.GetComponent<Animator>();
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if(Vector3.Distance(this.gameObject.transform.position, ComputerRoom.transform.position) < 0.1)
        {
            _aniController.SetBool("Walking", false);
            return;
        }

	    if(_targetLocation == Vector3.zero)
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
        }
	}
}
