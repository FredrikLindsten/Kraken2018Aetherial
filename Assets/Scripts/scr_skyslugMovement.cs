﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_skyslugMovement : MonoBehaviour {

    public float speed = 0;
    private float movement = 0;
    public float swarmDistance = 0;
    public float swarmDistanceMax = 0;
    public float swarmDistanceMin = 0;
    public float swarmTrigger = 0;

    private float randomDistance = 0;
    private float randomDistanceAcceleration = 0;

    public float attackCooldown = 0;
    private float attackTimer = 0;
    private bool attacking = false;
    private float attackMove = 0;

    private float distanceToPlayer = 0;
    private Vector2 playerPosition;

    private Vector2 circleVector;
    private float circlePosition = 0;

    private Vector2 destination;

    // Use this for initialization
    void Start () {
        attackTimer += Random.Range(0, attackCooldown);
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement = Time.deltaTime * speed;
        playerPosition = GameObject.Find("obj_player").transform.position - transform.position;
        distanceToPlayer = playerPosition.magnitude;
        attackTimer += Time.deltaTime;

        if (attackTimer > attackCooldown)
        {
            //attack player
            attacking = true;
        }
        if (distanceToPlayer < 0.8f)//TODO change for if collision
        {
            attackTimer = 0;
            attacking = false;
        }
        if (distanceToPlayer < swarmTrigger)
        {
            //swarm player
            circlePosition = Mathf.Atan2((-playerPosition.normalized).y, (-playerPosition.normalized).x);
            circlePosition += 2 * movement / swarmDistance;

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
            destination = playerPosition + circleVector;
        }
        else
        {
            //move toward player
            destination = playerPosition;
        }
        transform.Translate(destination.normalized * movement);
        //collision
    }
}
