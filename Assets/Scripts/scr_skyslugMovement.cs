﻿using System.Collections;
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

    public static bool visibility = true;
    
    private enum stateEnum { Hunting, Swarming, Attacking, Searching};
    private stateEnum state = stateEnum.Hunting;
    
    // Use this for initialization
    void Start () {
        attackTimer += Random.Range(0, attackCooldown);
	}

    private void OnDestroy()
    {
        scr_utilities.instance.Hide(0);
    }

    // Update is called once per frame
    void Update ()
    {
        movement = Time.deltaTime * speed;
        if (!visibility)
        {
            //lose sight
            state = stateEnum.Searching;
        }
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
            case stateEnum.Searching:
                Searching();
                break;
        }
        
        if (rotate)
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
        if (altrotate) { 
            transform.rotation = Quaternion.Euler(0, 0, altrotatemagnitude * (destination.normalized*movement).y - 90);
            if (state == stateEnum.Attacking && rotate)
                transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, destination));
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
        FindTarget(scr_utilities.player.transform.position);
        if (distanceToPlayer < swarmTrigger)
            state = stateEnum.Swarming;
        destination = vectorToPlayer;
    }

    void Swarming()
    {
        FindTarget(scr_utilities.player.transform.position);
        if (distanceToPlayer > swarmTrigger)
            state = stateEnum.Hunting;
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
        FindTarget(scr_utilities.player.transform.position);

        attackMove -= Time.deltaTime * 2.5f;

        if (distanceToPlayer < 0.8f)//TODO change for if collision
        {
            attackTimer = 0;
            state = stateEnum.Swarming;
        }
        FindPointOnCircle();
    }

    void Searching()
    {
        if (visibility)
        {
            state = stateEnum.Hunting;
        }
    }
}
