using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_leviathanhitbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnDestroy()
    {
        GetComponentInParent<scr_leviathan>().Leave();
        GetComponentInParent<scr_hpsystem>().takeDamage(1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
