using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_hpsystem : MonoBehaviour {
    public int health;
    private int maxhealth;
    public float invincibilityTime;
    private float time;
    private bool invincible;
    public Color std = Color.white;

    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;

	// Use this for initialization
	 protected void HpInit () {
        time = 1.0f;
        invincible = false;
        maxhealth = health;
	}
	
    protected void HpUpdate()
    {
        time += Time.deltaTime;
        if (time >= invincibilityTime)
        {
            invincible = false;
            if(gameObject.GetComponent<Renderer>().material.color == Color.red)
                gameObject.GetComponent<Renderer>().material.color = std;
        }
        else
        {
            invincible = true;
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        HpUpdate();
	}

    public void takeDamage(int damage)
    {
        if (invincible == false )
        {
            if (hitSound != null && audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(hitSound);
            }
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            health -= damage;
            time = 0.0f;
            scr_hpsystem parent = gameObject.GetComponentInParent<scr_hpsystem>();
            if (parent != this)
                parent.takeDamage(damage);
            if (getHealth() <= 0)
            {
                if (deathSound != null && audioSource.isPlaying == false)
                {
                    audioSource.PlayOneShot(deathSound);
                }
                Die();
            }
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void LoseSight() {}
    public virtual void GainSight() {}

    public int getHealth()
    {
        return health;
    }

    public float getHealthPercent()
    {
        return (float)health / (float)maxhealth;
    }

    public void healDamage(int healing)
    {
        health += healing;
    }
}
