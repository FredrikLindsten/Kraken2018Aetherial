using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_leviathanFinale : MonoBehaviour {

    public Vector3 position;
    public float Cooldown;
    public float BreathWeight;
    public float RamWeight;
    public float SummonWeight;
    public GameObject spawner;
    public float rammingSpeed;
    private float rammingSpeedActual;
    public float timeToFullRamSpeed;
    public float ramDistanceLimit;

    public GameObject soundwaveRef;

    private GameObject mouth;
    private bool animating;

    //private float randomMovement;
    //private float randomMovementAcc;

    private enum StateEnum { Idling, BreathAttack, CallForHelp, Ram, Waiting }
    private StateEnum state = StateEnum.Idling;
    private scr_leviathan main;

    private float timer = 0;

    private void Awake()
    {
        main = GetComponent<scr_leviathan>();
        mouth = GetComponentInChildren<scr_mouthHitbox>().gameObject;
        mouth.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        transform.position = position;
    }

    private void PickAttack()
    {
        float roll = Random.Range(0, BreathWeight + RamWeight + SummonWeight);
        if (roll < BreathWeight)
        {
            StartCoroutine(BreathAttack());
            state = StateEnum.BreathAttack;
        }
        else if (roll < BreathWeight + RamWeight)
        {
            StartCoroutine(RamAttack());
            state = StateEnum.Ram;
        }
        else
        {
            StartCoroutine(CallForHelp());
            state = StateEnum.CallForHelp;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //float accRange = 0.03f * Time.deltaTime;
        //randomMovementAcc += Random.Range(-accRange, accRange);
        //randomMovementAcc *= 0.85f;
        //randomMovement += randomMovementAcc;
        //transform.Translate(new Vector3(0, randomMovement - transform.position.y * 0.1f, 0));


        switch (state)
        {
            case StateEnum.Idling:
                timer += Time.deltaTime;
                if (timer > Cooldown)
                {
                    timer -= Cooldown;
                    PickAttack();
                }
                break;
            case StateEnum.BreathAttack:
                break;
            case StateEnum.CallForHelp:
                break;
            case StateEnum.Ram:
                break;
            case StateEnum.Waiting:
                break;
        }

    }

    IEnumerator CallForHelp()
    {
        //TODO balance/balance tools

        StartCoroutine(Animate(2));
        while (animating)
        {
            yield return null;
        }

        Instantiate(soundwaveRef, transform);

        scr_spawner spawn = Instantiate(
            spawner, 
            new Vector2(
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Left,false),
                    0), 
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Bottom,false), 
                    scr_utilities.GetEdge(edgeId.Top,false))), 
            Quaternion.identity).GetComponent<scr_spawner>();
        spawn.number = Random.Range(5, 20);
        spawn.spawnId = scr_spawner.SpawnerEnum.Skyslug;
        
        spawn = Instantiate(
            spawner,
            new Vector2(
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Left, false),
                    0),
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Bottom, false),
                    scr_utilities.GetEdge(edgeId.Top, false))),
            Quaternion.identity).GetComponent<scr_spawner>();
        spawn.number = Random.Range(5, 20);
        spawn.spawnId = scr_spawner.SpawnerEnum.Skyslug;


        spawn = Instantiate(
            spawner,
            new Vector2(
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Left, false),
                    0),
                Random.Range(
                    scr_utilities.GetEdge(edgeId.Bottom, false),
                    scr_utilities.GetEdge(edgeId.Top, false))),
            Quaternion.identity).GetComponent<scr_spawner>();
        spawn.number = Random.Range(1, 3);
        spawn.spawnId = scr_spawner.SpawnerEnum.Ray;

        state = StateEnum.Idling;
    }

    IEnumerator BreathAttack()
    {
        //TODO balance/balance tools

        StartCoroutine(Animate(2));
        while (animating)
        {
            yield return null;
        }

        state = StateEnum.Idling;
    }

    IEnumerator RamAttack()
    {
        //float animationDelay = 0;
        //yield return new WaitForSeconds(animationDelay);
        int std = main.collisionDamage;
        main.collisionDamage = 10;
        StartCoroutine(SpeedUp());
        Vector3 toPlayer = scr_utilities.player.transform.position - transform.position;
        toPlayer.y *= 0.8f;
        float dir = 1;
        while (true)
        {
            float move = rammingSpeedActual * Time.deltaTime * dir;
            transform.Translate(toPlayer.normalized * move);
            if (transform.position.x < ramDistanceLimit)
            {
                dir = -1;
                rammingSpeedActual = 2;
            }
            if(transform.position.x > position.x)
            {
                rammingSpeedActual = 0;
                transform.position = position;
                break;
            }
            yield return null;
        }
        
        state = StateEnum.Idling;
        main.collisionDamage = std;
    }

    IEnumerator SpeedUp()
    {
        while(rammingSpeedActual < rammingSpeed)
        {
            rammingSpeedActual += Time.deltaTime * rammingSpeed / timeToFullRamSpeed;
            yield return null;
        }
        rammingSpeedActual = rammingSpeed;
    }

    IEnumerator Animate(float time)
    {
        animating = true;
        mouth.SetActive(true);
        GetComponent<Animator>().SetBool("attack", true);
        yield return new WaitForSeconds(time);
        animating = false;
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().SetBool("attack", false);
        yield return new WaitForSeconds(1);
        mouth.SetActive(false);
    }

    private void OnDestroy()
    {
        //Instantiate gibs

    }
}
