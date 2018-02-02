using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_skyslugMovement : MonoBehaviour {

    public float speed = 0;
    private float movement = 0;
    public float swarmDistance = 0;
    public float swarmTrigger = 0;
    public float swarmDistanceMax = 0;
    public float swarmDistanceMin = 0;

    private float randomDistance = 0;
    private float randomDistanceAcceleration = 0;

    public bool rotate = false;
    public bool altrotate = false;
    public float altrotatemagnitude = 100;

    public float attackCooldown = 0;
    private float attackTimer = 0;
    private bool attacking = false;
    private float attackMove = 0;

    private float distanceToPlayer = 0;
    private Vector2 playerPosition;
    private Vector2 vectorToPlayer;

    private Vector2 circleVector;
    private float circlePosition = 0;

    private Vector2 destination;

    public static bool visibility = true;

    // Use this for initialization
    void Start () {
        attackTimer += Random.Range(0, attackCooldown);
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement = Time.deltaTime * speed;
        if (visibility)
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            attackTimer += Time.deltaTime;
        }
        else
        {
            attackTimer = 0;
            attacking = false;
        }
        vectorToPlayer  = playerPosition - (Vector2)transform.position;
        distanceToPlayer = vectorToPlayer.magnitude;

        if (attackTimer > attackCooldown)
        {
            //attack player
            attacking = true;
        }
        if (distanceToPlayer < 0.8f)//TODO change for if collision
        {
            scr_utilities.instance.Hide(20);
            attackTimer = 0;
            attacking = false;
        }
        if (distanceToPlayer < swarmTrigger)
        {
            //swarm player
            circlePosition = Mathf.Atan2((-vectorToPlayer.normalized).y, (-vectorToPlayer.normalized).x);
            circlePosition += 2 * movement / swarmDistance;

            //TODO This block needs fewer magic values and better handling of when the slug moves out of range
            randomDistanceAcceleration += Random.Range(-1.0f, 1.0f) / 100;
            randomDistance += randomDistanceAcceleration;
            if (randomDistance < swarmDistanceMin || randomDistance > swarmDistanceMax)
                randomDistanceAcceleration = 0;

            if (attacking)
                attackMove -= Time.deltaTime * 2.5f;
            if (!attacking && attackMove < 0)
                attackMove += Time.deltaTime * 3.5f;

            circleVector.x = Mathf.Cos(circlePosition);
            circleVector.y = Mathf.Sin(circlePosition);
            circleVector *= swarmDistance + randomDistance + attackMove;
            destination = vectorToPlayer + circleVector;
        }
        else
        {
            //move toward player
            destination = vectorToPlayer;
        }

        if (rotate)
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
        if (altrotate) { 
            transform.rotation = Quaternion.Euler(0, 0, altrotatemagnitude * (destination.normalized*movement).y - 90);
            if (attacking && rotate)
                transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
        }
        transform.Translate(destination.normalized * movement, Space.World);
        //collision
    }
}
