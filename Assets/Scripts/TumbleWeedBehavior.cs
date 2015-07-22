using UnityEngine;
using System.Collections;

public class TumbleWeedBehavior : MonoBehaviour {

    private float _rollSpeed;

    private int _range;
    private Vector3 _waypoint;


    private float _timer;
    private bool _idleMode;

    private float _rollTime;
    private float _idleTime;

    private float _rayLength;


    // Use this for initialization
    void Start()
    {
        _range = 10;
        _rollSpeed = 3;
        _rayLength = 0.5f;

        //set idle and roll time
        setNewTime();

        //overall timer
        _timer = 0;

        //initialize first Waypoint
        getNewWayPoint();
    }

    // Update is called once per frame
    void Update()
    {

        checkCollision();
        roll();
        

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
     * Roll and Idle cycle
     **/
    private void roll()
    {
        //check for collision before roll, if collided --> new waypoint
        //roll as long as the timer suggests
        if (_timer <= _rollTime)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * _rollSpeed * Time.deltaTime;
            rotate();
            

        }

        //tell when weed is in idle Mode
        if (_timer > _rollTime && _timer <= (_rollTime + _idleTime))
        {
            _idleMode = true;

        }

        //reset timer after one roll and idle cycle is done
        if (_timer >= (_rollTime + _idleTime))
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

    private void rotate()
    {

        transform.Find("Ball").Rotate(Vector3.right * Time.deltaTime * 50f);
    }

    private void checkCollision()
    {
        if (Physics.SphereCast(new Ray(transform.position + 0.1f * transform.forward, transform.forward), 1, _rayLength))
        {
            getNewWayPoint();
        }
    }

    /**
     * Sets a random rolling and idling time
     **/
    private void setNewTime()
    {
        _rollTime = Random.Range(2, 7);
        _idleTime = Random.Range(2, 5);
    }
}
