using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_skyslugMovement : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip attackingClip;
    bool attackSoundPlayed;

    public float speed = 0;
    private float movement = 0;
    public float swarmDistance = 0;
    public float swarmTrigger = 0;
    public float swarmDistanceMax = 0;
    public float swarmDistanceMin = 0;

    private float randomDistance = 0;
    private float randomDistanceAcceleration = 0;
    public float randomDistanceSpeed = 0;

    public bool rotate = false;
    public bool altrotate = false;
    public float altrotatemagnitude = 100;

    public float attackCooldown = 0;
    private float attackTimer = 0;
    private float attackMove = 0;

    private float distanceToPlayer = 0;
    private Vector2 vectorToPlayer;

    private Vector2 circleVector;
    private float circleAngle = 0;

    private Vector2 destination;
    private int targetId;

    private bool visibility = true;
    public void LoseSight()
    {
        if (!visibility)
            return;
        visibility = false;
        targetId = Random.Range(0, scr_powerup.instance.nrOfGhosts);
        swarmDistance -= 1;
        speed *= 0.5f;
    }
    public void GainSight()
    {
        if (visibility)
            return;
        visibility = true;
        swarmDistance += 1;
        speed *= 2;
    }
    
    private enum stateEnum { Hunting, Swarming, Attacking};
    private stateEnum state = stateEnum.Hunting;
    
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        attackTimer += Random.Range(0, attackCooldown);
        scr_utilities.slugs.Add(this);
        attackSoundPlayed = false;
	}

    // Update is called once per frame
    void Update ()
    {
        if (Time.timeScale == 0)
            return;
        movement = Time.deltaTime * speed;
        if (visibility)
            FindTarget(scr_utilities.player.transform.position);
        else
            FindTarget(scr_powerup.instance.GetGhost(targetId));
        switch (state)
        {
            case stateEnum.Hunting:
                Hunting();
                break;
            case stateEnum.Swarming:
                Swarming();
                break;
            case stateEnum.Attacking:
                Attacking();
                break;
        }
        
        if (rotate)
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
        if (altrotate) { 
            if (state == stateEnum.Attacking && rotate)
                transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
            transform.rotation = Quaternion.Euler(0, 0, altrotatemagnitude * ((destination.normalized*movement).y - (destination.normalized * movement).x));
        gameObject.GetComponent<Animator>().speed = 0.8f + (destination.normalized.x/2);
        }
        transform.Translate(destination.normalized * movement, Space.World);
        //TODO collision
    }

    void UpdateRandomMovement()
    {
        float accRange = 0.01f * Time.deltaTime * randomDistanceSpeed;
        randomDistanceAcceleration += Random.Range(-accRange, accRange);

        if (randomDistance < swarmDistanceMin || randomDistance > swarmDistanceMax)
            randomDistanceAcceleration *= -1;
        randomDistance += randomDistanceAcceleration;
        randomDistanceAcceleration *= 0.9f;
    }

    void FindTarget(Vector2 target)
    {
        vectorToPlayer = target - (Vector2)transform.position;
        distanceToPlayer = vectorToPlayer.magnitude;
    }

    void FindPointOnCircle()
    {
        circleAngle = Mathf.Atan2(-vectorToPlayer.y, -vectorToPlayer.x);
        circleAngle += movement / swarmDistance;

        UpdateRandomMovement();

        circleVector.x = Mathf.Cos(circleAngle);
        circleVector.y = Mathf.Sin(circleAngle);
        circleVector *= swarmDistance + randomDistance + attackMove;
        destination = vectorToPlayer + circleVector;
    }

    void Hunting()
    {
        if (distanceToPlayer < swarmTrigger)
            state = stateEnum.Swarming;
        destination = vectorToPlayer;
    }

    void Swarming()
    {
        if (distanceToPlayer > swarmTrigger)
            state = stateEnum.Hunting;
        if(visibility)
            attackTimer += Time.deltaTime;
        if (attackTimer > attackCooldown)
        {
            //attack player
            state = stateEnum.Attacking;
        }
        if(attackMove < 0)
            attackMove += Time.deltaTime * 3.5f;
        FindPointOnCircle();
    }

    void Attacking()
    {
        attackMove -= Time.deltaTime * 2.5f;

        if(attackSoundPlayed == false)
        {
            audioSource.PlayOneShot(attackingClip);
            attackSoundPlayed = true;
        }


        if (distanceToPlayer < 0.6f)//TODO change for if collision
        {
            attackTimer = 0;
            state = stateEnum.Swarming;
            attackSoundPlayed = false;
        }
        FindPointOnCircle();
    }
}
