using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_weapon : MonoBehaviour {
    // Use this for initialization


    void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dir.Normalize();
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
        Debug.DrawRay(transform.position, transform.right);
    }
}
