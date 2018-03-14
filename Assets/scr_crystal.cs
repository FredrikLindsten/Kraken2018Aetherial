using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_crystal : scr_hpsystem {
    // Use this for initialization
    
	void Start ()
    {
        transform.localScale = new Vector3(0.2f / transform.parent.localScale.x, 0.2f / transform.parent.localScale.y, 1);
        GetComponent<SpriteRenderer>().sprite = scr_cloudcontroller.instance.crystalArt;
        audioSource = GetComponent<AudioSource>();
	}

    protected override void Die()
    {
        GetComponent<Animator>().SetBool("destroyed", true);
        scr_utilities.instance.aetherLeft += 10;
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Harpoon" && other.GetComponent<Harpoon>().isFired == true)
        {
            audioSource.Play();
        }
    }
}
