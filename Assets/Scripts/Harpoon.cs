using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour {
    private bool fired;
    new Transform transform;
    new Rigidbody2D rigidbody;
    public float reloadTime;
    private float reloadTimer;
    new Collider2D collider;
    public float speed;
    bool reloaded;
    enum STATE { RELOADED, FIRED, STUCK };
    private GameObject crystal;
    public Sprite stuckSprite;
    public Sprite defaultSprite;
    private float crystalOffsetX = -0.5f; //crystal sprite isn't perfectly alligned so I have to use an offset
    private float crystalOffsetY = -0.2f;
    public GameObject chain;
    STATE harpState;

    AudioSource audioSource;
    public AudioClip firingClip;
    public AudioClip hittingClip;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        harpState = STATE.RELOADED;
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        reloadTimer = 0.0f;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (harpState == STATE.FIRED)
        {
            rigidbody.velocity = transform.right * speed;
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                harpState = STATE.RELOADED;
                reloadTimer = 0.0f;
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
        if (harpState == STATE.RELOADED)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            transform.position = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>().position;
        }
        if (harpState == STATE.STUCK)
        { 
            if (!GameObject.FindGameObjectWithTag("Chain"))
            {
                Instantiate(chain);
            }
            
            if (crystal != null)
            {
                transform.position = new Vector3(crystal.transform.position.x + crystalOffsetX, crystal.transform.position.y + crystalOffsetY, crystal.transform.position.z);
            } else
            {
                harpState = STATE.RELOADED;
            }

        } else if (GameObject.FindGameObjectWithTag("Chain"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Chain"));
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().GetBool("destroyed") == true)
        {
            Destroy(this.gameObject);
        }
	}


    public void Fire ()
    {
        if (harpState == STATE.RELOADED)
        {
            audioSource.PlayOneShot(firingClip);
            harpState = STATE.FIRED;
        }
        if (harpState == STATE.STUCK)
        {

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (harpState == STATE.FIRED)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<scr_hpsystem>().takeDamage(5);
            }
            if (other.gameObject.tag == "BossCrystal")
            {
                stuck(other.gameObject);
                other.gameObject.GetComponentInChildren<scr_armourplate>().harpoonHit(Mathf.Abs(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)));
                /* Destroy(other.gameObject);
                 scr_utilities.leviathan.GetComponentInChildren<scr_leviathan>().Leave();
                 scr_utilities.instance.powerUpIndicator.enabled = true;*/
            }
            if (other.gameObject.tag == "Crystal")
            {
                other.GetComponent<scr_hpsystem>().takeDamage(10);
            }
        }
        

    }

    public void stuck (GameObject tempcrystal)
    {
        crystal = tempcrystal;
        harpState = STATE.STUCK;
        gameObject.GetComponent<SpriteRenderer>().sprite = stuckSprite;
        audioSource.PlayOneShot(hittingClip);
    }
}
