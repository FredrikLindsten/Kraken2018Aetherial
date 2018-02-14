using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour {

    public int damage;
    public Vector3 originPoint;
    private Vector3 endPoint;
    LineRenderer lineRenderer;
    new BoxCollider2D collider;
    public float beamLength;
    public PhysicsMaterial2D material;
	// Use this for initialization
	void Start () {
        transform.rotation = GameObject.FindGameObjectWithTag("Weapon").GetComponent<scr_weapon>().transform.rotation;
        gameObject.tag = "Beam";
        originPoint = GameObject.FindGameObjectWithTag("Weapon").GetComponent<scr_weapon>().transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.sharedMaterial = material;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        scr_utilities.instance.aetherLeft -= 1 * Time.deltaTime;
        if (scr_utilities.instance.aetherLeft < 0)
            Destroy(this.gameObject);
        originPoint = GameObject.FindGameObjectWithTag("Weapon").GetComponent<scr_weapon>().transform.position;
        lineRenderer.SetPosition(0, originPoint);
        lineRenderer.SetPosition(1, endPoint = originPoint + transform.right * beamLength);
        transform.rotation = GameObject.FindGameObjectWithTag("Weapon").GetComponent<scr_weapon>().transform.rotation;
        collider.size = new Vector2(beamLength, lineRenderer.startWidth * 5);
        //collider.size = new Vector2(originPoint.x, lineRenderer.startWidth);
        collider.transform.position = originPoint + (endPoint - originPoint) / 2;
     
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<scr_hpsystem>())
            {
                other.GetComponent<scr_hpsystem>().takeDamage(damage);
            }
        }
    }
}
