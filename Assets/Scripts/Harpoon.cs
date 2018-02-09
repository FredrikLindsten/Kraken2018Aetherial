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
    STATE harpState;
    // Use this for initialization
    void Start () {
        harpState = STATE.RELOADED;
        rigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        reloadTimer = 0.0f;
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
	}
	
	// Update is called once per frame
	void Update () {

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
            if (crystal != null)
            {
                transform.position = new Vector3(crystal.transform.position.x + crystalOffsetX, crystal.transform.position.y + crystalOffsetY, crystal.transform.position.z);
            } else
            {
                harpState = STATE.RELOADED;
            }

        }
	}


    public void Fire ()
    {
        if (harpState == STATE.RELOADED)
        {
            harpState = STATE.FIRED;
        }
        if (harpState == STATE.STUCK)
        {

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<scr_hpsystem>().takeDamage(5);
        }
        if(other.gameObject.tag == "BossCrystal")
        {
            stuck(other.gameObject);
            other.GetComponentInChildren<scr_armourplate>().harpoonHit(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position));
        }
       
    }

    public void stuck (GameObject tempcrystal)
    {
        crystal = tempcrystal;
        harpState = STATE.STUCK;
        gameObject.GetComponent<SpriteRenderer>().sprite = stuckSprite;
    }
}
