using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_skyslugMovement : MonoBehaviour {

    public float speed = 0;
    private float movement = 0;
    public float swarmDistance = 0;
    public float swarmTrigger = 0;

    private float distanceToPlayer = 0;
    private Vector2 playerPosition;

    private Vector2 circleVector;
    private float circlePosition = 0;

    private Vector2 destination;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        movement = Time.deltaTime * speed;
        playerPosition = GameObject.Find("nameofplayerobject").transform.position - transform.position;
        distanceToPlayer = playerPosition.magnitude;
        if (distanceToPlayer < swarmTrigger)
        {
            //swarm player
            circlePosition = Mathf.Cos(-playerPosition.normalized.x);
            
            circlePosition += movement / swarmDistance;

            circleVector.x = Mathf.Cos(circlePosition);
            circleVector.y = Mathf.Sin(circlePosition);
            circleVector *= swarmDistance; //TODO randomize
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
