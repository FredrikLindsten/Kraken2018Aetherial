using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathattack : MonoBehaviour {
    public int damage;
    new PolygonCollider2D collider2D;
	// Use this for initialization
	void Start () {
        collider2D = GetComponent<PolygonCollider2D>();
        collider2D.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<scr_hpsystem>().takeDamage(damage);
        }
    }

    void InstantiateTrigger()
    {
        collider2D.enabled = true;
    }

    void DisableTrigger()
    {
        collider2D.enabled = false;
    }

    void destroySelf()
    {
        Destroy(this.gameObject);
    }
}
