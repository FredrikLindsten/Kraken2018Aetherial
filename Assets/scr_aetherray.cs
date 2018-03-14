using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_aetherray : MonoBehaviour {
    
    public float dashCooldown = 0;
    private float dashTimer = 0;
    public float attackCooldown = 0;
    private float attackTimer = 0;
    public float minDist = 0;
    public float maxDist = 0;//AKA attack range
    public float dashSpeed = 0;
    public float attackSpeed = 0;
    public int damage = 0;

    public GameObject bolt;

    private enum State { attacking, dashing, idling, waiting}
    private State currentState = State.idling;

    private float randomDistanceX = 0;
    private float randomDistanceY = 0;
    private float randomDistanceAccelerationX = 0;
    private float randomDistanceAccelerationY = 0;

    private Vector3 dest = new Vector3();
    private Vector3 orig = new Vector3();
    private Vector3 move = new Vector3();
    private float dist = 0;
    private float dashProgress = 0;

    AudioSource audioSource;
    public AudioClip shootSound;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        currentState = State.idling;
        dashTimer += Random.Range(0, dashCooldown);
        attackTimer += Random.Range(0, attackCooldown);

        currentState = State.waiting;
        StartCoroutine(TeleportBehaviour());
    }

    void RandomMovement()
    {
        float accRange = 0.02f * Time.deltaTime;
        randomDistanceAccelerationY += Random.Range(-accRange, accRange);
        randomDistanceAccelerationX += Random.Range(-accRange, accRange);

        randomDistanceY += randomDistanceAccelerationY;
        randomDistanceAccelerationY *= 0.90f;
        randomDistanceY *= 0.90f;
        randomDistanceX += randomDistanceAccelerationX;
        randomDistanceAccelerationX *= 0.90f;
        randomDistanceX *= 0.90f;

        transform.Translate(randomDistanceX, randomDistanceY, 0);
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.idling:
                RandomMovement();
                dashTimer += Time.deltaTime;
                attackTimer += Time.deltaTime;
                float distance = (scr_utilities.player.transform.position - transform.position).magnitude;
                if (distance < minDist)
                    dashTimer += Time.deltaTime * (minDist / distance);
                if (dashTimer > dashCooldown)
                {
                    currentState = State.waiting;
                    StartCoroutine(TeleportBehaviour());
                    dashTimer = 0;
                }

                if(attackTimer > attackCooldown && distance < maxDist && GameObject.FindGameObjectWithTag("Player").GetComponent<scr_powerup>().powerOn == false) // only shoot when powerup is not on
                {
                    currentState = State.attacking;
                    StartCoroutine(Attack());
                    attackTimer = 0;
                }
                break;
            case State.attacking:
                break;
            case State.dashing:
                Dashmove();
                break;
            case State.waiting:
                break;
        }
	}

    IEnumerator Attack()
    {
        GetComponent<Animator>().SetBool("attack", true);
        audioSource.PlayOneShot(shootSound);
        yield return new WaitForSeconds(attackSpeed);
        GetComponent<Animator>().SetBool("attack", false);
        currentState = State.idling;
        if ((scr_utilities.player.transform.position - transform.position).magnitude > maxDist)
            yield break;
        Instantiate(bolt, gameObject.transform);
        //scr_utilities.player.GetComponent<scr_hpsystem>().takeDamage(damage);

    }

    private void Dashmove()
    {
        float movement = dashSpeed * Time.deltaTime;
        dashProgress += movement / move.magnitude;
        transform.position = orig + (move * Mathf.Clamp(dashProgress,0,1));
        if (dashProgress > 1)
        {
            dashProgress = 0;
            currentState = State.idling;
        }
    }

    private bool FindTargetLocation()
    {
        for(int i = 0; i < 10; ++i)
        {
            dest.x = Random.Range(scr_utilities.GetEdge(edgeId.Left,false), scr_utilities.GetEdge(edgeId.Right, false));
            dest.y = Random.Range(scr_utilities.GetEdge(edgeId.Bottom,false), scr_utilities.GetEdge(edgeId.Top,false));
            dist = (dest - scr_utilities.player.transform.position).magnitude;
            if (minDist < dist && dist < maxDist)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator TeleportBehaviour()
    {
        while(true)
        {
            yield return null;
            if (FindTargetLocation())
            {
                orig = transform.position;
                move = dest - orig;
                currentState = State.dashing;
                break;
            }
        }
    }
}
