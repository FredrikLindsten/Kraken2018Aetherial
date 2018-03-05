using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayprojectile : MonoBehaviour {
    public int damage;
    public float speed;
    public float lifeTimeLimit;
    private float lifeTime;
    new private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        lifeTime = 0.0f;
        transform.position = transform.parent.position + new Vector3(0.936f, 0.32f, 0);
        transform.parent = null;
        Vector2 dir = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        dir.Normalize();
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, z);
        Debug.DrawRay(transform.position, transform.right);

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        lifeTime += Time.deltaTime;
        rigidbody.velocity = transform.right * speed;
        if (lifeTime >= lifeTimeLimit)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<scr_hpsystem>().takeDamage(damage);
        }
    }
}
