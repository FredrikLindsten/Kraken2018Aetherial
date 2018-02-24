using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_stormcloud : MonoBehaviour {

    public float speed = 0;
    public bool wait = false;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x < 0 && !wait) { 
            speed = 0;
            wait = true;
        }
        if(speed < 0.001f)
            gameObject.GetComponentInChildren<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(-Time.time * scr_cloud.speed/80, 0));

        if (transform.position.x < -(scr_utilities.screenWidth + (2 * scr_utilities.padding)))
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<scr_skyslugMovement>().LoseSight();

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<scr_skyslugMovement>().GainSight();
        }
        other.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        other.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
    }
}
