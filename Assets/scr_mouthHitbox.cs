using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_mouthHitbox : MonoBehaviour {

    scr_hpsystem hp;

	// Use this for initialization
	void Start () {
        hp = GetComponentInParent<scr_hpsystem>();
        hp.health = 3;
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Harpoon")
        {
            hp.takeDamage(1);
        }
    }
}
