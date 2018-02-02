using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_hpsystem : MonoBehaviour {
    public int health;
    public float invincibilityTime;
    private float time;
    private bool invincible;
	// Use this for initialization
	void Start () {
        time = 0.0f;
        invincible = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        time += Time.deltaTime;
        if (time >= invincibilityTime)
        {
            invincible = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        } else
        {
            invincible = true;
        }
        if (getHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void takeDamage(int damage)
    {
        if (invincible == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            health -= damage;
            time = 0.0f;
        }



    }

    public int getHealth()
    {
        return health;
    }

    public void healDamage(int healing)
    {
        health += healing;
    }
}
