using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerupPickup : MonoBehaviour {
    bool taken = false;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, Time.deltaTime * Mathf.Abs(transform.position.y), 0);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !taken)
        {
            taken = true;
            audioSource.PlayOneShot(audioSource.clip);
            scr_powerup.instance.gainPowerup();
            Destroy(this.gameObject,audioSource.clip.length);
            //play anim
        }
    }
}
