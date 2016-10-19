using UnityEngine;
using System.Collections;

public class CreatureBehavior : MonoBehaviour {

    private float _walkSpeed;
    private float _jumpSpeed;
    private float _jumpHeight;

    private int _range;
    private Vector3 _waypoint;


    private float _timer;
    private bool _idleMode;
    private bool _jumpMode;

    private float _walkTime;
    private float _idleTime;

    private float _rayLength;


	// Use this for initialization
	void Start () {
        _range = 10;
        _walkSpeed = 3;
        _jumpSpeed = 100.0f;
        _rayLength = 0.5f;

        _jumpMode = false;
        _jumpHeight = 2;

        //set idle and walk time
        setNewTime();
        
        //overall timer
        _timer = 0;

        //initialize first Waypoint
        getNewWayPoint();
	}
	
	// Update is called once per frame
	void Update () 
    {



        checkCollision();
        walk();
        
	}

    void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
    
    }

    private void getNewWayPoint()
    {
        _waypoint = new Vector3(Random.Range(transform.position.x - _range, transform.position.x + _range), 1, Random.Range(transform.position.z - _range, transform.position.z + _range));
        _waypoint.y = 0;
        transform.LookAt(_waypoint);
        //Debug.Log(_waypoint + " and " + (transform.position - _waypoint).magnitude);
    }

    /**
     * Walk and Idle cycle
     **/
    private void walk()
    {
        //check for collision before walk, if collided --> new waypoint
        //walk as long as the timer suggests
        if (_timer <= _walkTime)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * _walkSpeed * Time.deltaTime;
            _jumpMode = true;
            jump();
            
        }

        //tell when creature is in idle Mode
        if (_timer > _walkTime && _timer <= (_walkTime + _idleTime))
        {
            _idleMode = true;
            
        }

        //reset timer after one walk and idle cycle is done
        if (_timer >= (_walkTime + _idleTime))
        {
            _timer = 0;
            _idleMode = false;
            //set a new random time for idle and walk cycle duration
            setNewTime();
        }

        if ((transform.position - _waypoint).magnitude < 3)
        {
            // when the distance between us and the target is less than 3
            // create a new way point target

            getNewWayPoint();
        }

    }

    private void jump()
    {

        //if (this.transform.position.y <= _jumpHeight && _jumpMode == true)
        if (_jumpMode)
        {
            _jumpMode = false;
            //this.transform.Translate(Vector3.up * _jumpSpeed * Time.deltaTime);
        }
        else
        {
            this.transform.Translate(Vector3.down * Time.deltaTime);
            _jumpMode = true;
        }
        //after reaching height, set jumpode to false
        //if (this.transform.position.y > _jumpHeight && this.transform.position.y > 0)
        //{
        //    _jumpMode = false;
        //    this.transform.Translate(Vector3.up * Time.deltaTime);
        //}
        //Debug.Log(this.transform.position.y);
    }

    private void checkCollision()
    {

        if (Physics.SphereCast(new Ray(transform.position + 0.1f * transform.forward, transform.forward), 1, _rayLength))
        {
            Debug.Log("I've hit something.");
            getNewWayPoint();
        }
    }

    /**
     * Sets a random walking and idling time
     **/
    private void setNewTime()
    {
        _walkTime = Random.Range(2, 7);
        _idleTime = Random.Range(2, 5);
    }
}
