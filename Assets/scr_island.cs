using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_island : MonoBehaviour {

    private float timer;
    public float damageTime;

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = scr_cloudcontroller.instance.islandArt;
        timer = 0.0f;
        transform.Translate(0,0,-1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;
        if(collision.tag == "Player")
        {
            //collision.GetComponent<Rigidbody2D>().AddForce(collision.transform.position - transform.position);
            //collision.GetComponent<Rigidbody2D>().velocity += (Vector2)(collision.transform.position - transform.position);
            Vector2 islandToPlayer = (Vector2)(collision.transform.position - transform.position);
            if (Mathf.Abs(islandToPlayer.x) > Mathf.Abs(islandToPlayer.y))
                collision.transform.position = new Vector3(collision.transform.position.x, transform.position.y + (Mathf.Sign(islandToPlayer.y) * transform.localScale.y / 2), -3);
            else
                collision.transform.position = new Vector3(transform.position.x + (Mathf.Sign(islandToPlayer.x) * transform.localScale.x / 2), collision.transform.position.y, -3);
            if (timer >= damageTime)
            {
                collision.GetComponent<scr_hpsystem>().takeDamage(1);
                timer = 0.0f;
            }
           
        }
    }
}
