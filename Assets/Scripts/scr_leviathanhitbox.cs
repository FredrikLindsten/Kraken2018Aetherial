using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_leviathanhitbox : MonoBehaviour {

    public GameObject powerup = null;

	// Use this for initialization
	void Start () {
		
	}

    void OnDestroy()
    {
        GetComponentInParent<scr_leviathan>().Leave();
        GetComponentInParent<scr_hpsystem>().takeDamage(1);
        Instantiate(powerup, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
