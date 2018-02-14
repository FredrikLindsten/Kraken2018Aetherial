using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerupPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, Time.deltaTime * Mathf.Abs(transform.position.y), 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            scr_powerup.instance.gainPowerup();
            Destroy(this.gameObject);
            //play anim
        }
    }
}
