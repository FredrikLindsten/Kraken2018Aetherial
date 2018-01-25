using System.Collections;
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
        playerPosition = GameObject.Find("obj_player").transform.position - transform.position;
        distanceToPlayer = playerPosition.magnitude;
        if (distanceToPlayer < swarmTrigger)
        {
            //swarm player
            circlePosition = Mathf.Atan2((-playerPosition.normalized).y, (-playerPosition.normalized).x);
            circlePosition += movement / swarmDistance;

            randomDistance += Random.Range(-1.0f, 1.0f)/10;
            if (randomDistance < swarmDistanceMin)
                randomDistance = swarmDistanceMin;
            if (randomDistance > swarmDistanceMax)
                randomDistance = swarmDistanceMax;

            circleVector.x = Mathf.Cos(circlePosition);
            circleVector.y = Mathf.Sin(circlePosition);
            circleVector *= swarmDistance + randomDistance;
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
