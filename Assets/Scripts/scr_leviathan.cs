using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_leviathan : MonoBehaviour {

    public float timeToArrive = 0;
    private bool arrived = false;

    public int collisionDamage;
    
    public float speed = 0;
    private float movement = 0;
    private float randomDistanceX = 0;
    private float randomDistanceY = 0;
    private float randomDistanceAccelerationX = 0;
    private float randomDistanceAccelerationY = 0;

    private float targetAngle = 2.5f;
    float circleAngle = 0;

    private Vector3 destination;
    private Vector3 target;
    private Vector3 circleCenter;

    EdgeCollider2D edgeCollider;

    // Use this for initialization
    void Start () {
        edgeCollider = GetComponent<EdgeCollider2D>();
        circleCenter = new Vector3(transform.position.x, transform.position.y - 10, 0);
        transform.Translate(-10, -10, 0);
        target = transform.position;
        StartCoroutine(Timer());
	}

    bool ShouldMove()
    {
        return circleAngle > (targetAngle + 0.1f) || circleAngle < (targetAngle -0.1f);
    }

    public void Finale()
    {
        GetComponent<scr_leviathanFinale>().enabled = true;
        StopAllCoroutines();
        scr_cloud.SetSpeed(0);
        enabled = false;

    }

    void OnArrival()
    {
        if(!arrived)
        {
            randomDistanceX = 1;
            arrived = true;
        }
    }

    public void Appear()
    {
        targetAngle = Mathf.PI / 2;
    }

    public void Leave()
    {
        targetAngle = Mathf.PI;
        scr_utilities.instance.Victory();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToArrive);
        Appear();
    }

    void RandomMovement()
    {
        float accRange = 0.02f * Time.deltaTime;
        randomDistanceAccelerationY += Random.Range(-accRange, accRange);
        randomDistanceAccelerationX += Random.Range(-accRange, accRange);

        randomDistanceY += randomDistanceAccelerationY;
        randomDistanceAccelerationY *= 0.84f;
        randomDistanceY *= 0.88f;
        randomDistanceX += randomDistanceAccelerationX;
        randomDistanceAccelerationX *= 0.88f;
        randomDistanceX *= 0.85f;

        transform.Translate(randomDistanceX, randomDistanceY, 0);
    }

    void CircleMovement()
    {
        Vector3 target = new Vector3(0, 0, 0);
        Vector3 vectorFromCenter = transform.position - circleCenter;

        circleAngle = Mathf.Atan2(vectorFromCenter.y, vectorFromCenter.x);
        circleAngle -= movement / 10;

        target.x = Mathf.Cos(circleAngle);
        target.y = Mathf.Sin(circleAngle);
        target *= 10;
        destination = (-vectorFromCenter) + target;
        transform.Translate(destination.normalized * movement, Space.World);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Finale();
        movement = speed * Time.deltaTime;
        Vector3 vectorFromCenter = target - circleCenter;

        circleAngle = Mathf.Atan2(vectorFromCenter.y, vectorFromCenter.x);
        if (ShouldMove())
        {//TODO refactor
            circleAngle -= movement / 10;
            target.x = Mathf.Cos(circleAngle);
            target.y = Mathf.Sin(circleAngle);
            target *= 10;
            target += circleCenter;
            destination = target - transform.position;
            transform.Translate(destination.normalized * movement, Space.World);
        }
        RandomMovement();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            Physics2D.IgnoreCollision(edgeCollider, other.collider);

        }
        else
        {
            other.gameObject.GetComponent<scr_hpsystem>().takeDamage(collisionDamage);
        }
    }
}

