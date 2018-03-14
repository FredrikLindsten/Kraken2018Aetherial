using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_parallax : MonoBehaviour {

    public float speed;
    float movement;
    Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponentInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        movement += Time.deltaTime;
        rend.material.SetTextureOffset("_MainTex", new Vector2(movement * speed, 0));
    }
}
