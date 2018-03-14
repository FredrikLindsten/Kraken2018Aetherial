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
    public GameObject breathRef;

    public GameObject mouth;
    private bool animating;

    //private float randomMovement;
    //private float randomMovementAcc;

    private enum StateEnum { Idling, BreathAttack, CallForHelp, Ram, Waiting }
    private StateEnum state = StateEnum.Idling;
    private scr_leviathan main;

    private float timer = 0;

    AudioSource audioSource;
    public AudioClip callSound;

    private void Awake()
    {
        main = GetComponent<scr_leviathan>();
    }

    // Use this for initialization
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        transform.localScale = new Vector3(-1, 1, 1);
        transform.position = new Vector3(20, 0, 0);
        GetComponent<Animator>().SetBool("finale", true);
        StartCoroutine(Entry());
    }

    IEnumerator Entry()
    {
        GetComponent<scr_hpsystem>().invincibilityTime = Cooldown;
        state = StateEnum.Waiting;
        Vector3 toPos = (position - transform.position).normalized;

        float speed = 2;

        yield return new WaitForSeconds(5);

        while (scr_cloud.GetSpeed()>0.1)
        {
            scr_cloud.SetSpeed(scr_cloud.GetSpeed() - (Time.deltaTime * 2));
            yield return null;
        }
        scr_cloud.SetSpeed(0);
        while (transform.position.x > position.x)
        {
            transform.Translate(toPos * Time.deltaTime * speed);
            yield return null;
        }
        transform.position = position;
        state = StateEnum.Idling;
    }

    private void PickAttack()
    {
        float roll = Random.Range(0, BreathWeight + RamWeight + SummonWeight);
        if (roll < BreathWeight)
        {
            state = StateEnum.BreathAttack;
            StartCoroutine(BreathAttack());
        }
        else if (roll < BreathWeight + RamWeight)
        {
            state = StateEnum.Ram;
            StartCoroutine(RamAttack());
        }
        else
        {
            state = StateEnum.CallForHelp;
            StartCoroutine(CallForHelp());
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
                    gameObject.GetComponent<Renderer>().material.color = Color.white;
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

    public IEnumerator CallForHelp()
    {
        //TODO balance/balance tools

        StartCoroutine(Animate(2));
        while (animating)
        {
            yield return null;
        }

        audioSource.PlayOneShot(callSound);
        Instantiate(soundwaveRef, transform);
        yield return new WaitForSeconds(0.2f);
        Instantiate(soundwaveRef, transform);
        yield return new WaitForSeconds(0.2f);
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
        
    }

    IEnumerator BreathAttack()
    {
        //TODO balance/balance tools
        
        StartCoroutine(Animate(2));
        while (animating)
        {
            transform.Translate(0, Mathf.Sign(scr_utilities.player.transform.position.y - transform.position.y) * Time.deltaTime, 0);
            yield return null;
        }
        Transform breath = Instantiate(breathRef, transform).transform;
        breath.localPosition = new Vector3(14, -2, 0);
        breath.localScale = new Vector3(-3, 2, 0);
        while (Mathf.Abs(transform.position.y - position.y) > 0.01f)
            transform.Translate(0, Mathf.Sign(position.y - transform.position.y) * Time.deltaTime, 0);
        transform.position = position;
    }

    IEnumerator RamAttack()
    {
        //float animationDelay = 0;
        //yield return new WaitForSeconds(animationDelay);
        int std = main.collisionDamage;
        main.collisionDamage = 1;
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
        
        main.collisionDamage = std;
        state = StateEnum.Idling;
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
        yield return new WaitForSeconds(2);
        GetComponent<Animator>().SetBool("attack", false);
        yield return new WaitForSeconds(1);
        mouth.SetActive(false);
        state = StateEnum.Idling;
    }
}
