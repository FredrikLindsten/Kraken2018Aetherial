using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_armourplate : MonoBehaviour {
    public GameObject crystal;
    public GameObject hitbox;
    public float health;
    float hpcounter;
    bool harpStuck;
    public float pulloffset;
    float distanceToPlayer; //the distance to the player when hit

	// Use this for initialization
	void Start () {
        health = 2;
        harpStuck = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(harpStuck == true)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)) > Mathf.Abs(distanceToPlayer) + pulloffset)
            {
                hpcounter += Time.deltaTime;
                if (hpcounter >= health)
                {
                    Instantiate(hitbox, transform.parent.position, Quaternion.identity).transform.parent = transform.parent.parent;
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShake>().shakeCamera(0.1f, 1);
                    Destroy(transform.parent.gameObject);
                    Destroy(this.gameObject);
                }
            }
           
        }
	}

    public void harpoonHit(float distance)
    {
        distanceToPlayer = distance;
        harpStuck = true;
    }
}
