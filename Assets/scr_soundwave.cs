using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_soundwave : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
	}
	
	// Update is called once per frame
	void Update () {

    }
    
}
