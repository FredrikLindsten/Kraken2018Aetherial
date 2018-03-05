using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_island : scr_cloud {

    private float timer;
    public float damageTime;

    private GameObject crystal;

    // Use this for initialization
    void Start()
    {
        height = Random.Range(
                scr_utilities.GetEdge(edgeId.Bottom, true),
                scr_utilities.GetEdge(edgeId.Top, true));
        GetComponent<SpriteRenderer>().sprite = scr_cloudcontroller.instance.islandArt;
        timer = 0.0f;
        transform.Translate(0, 0, 1);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;
        if(collision.tag == "Player")
        {
            //collision.GetComponent<Rigidbody2D>().AddForce(collision.transform.position - transform.position);
            //collision.GetComponent<Rigidbody2D>().velocity += (Vector2)(collision.transform.position - transform.position);
            Vector2 islandToPlayer = (Vector2)(collision.transform.position - transform.position);
            if (Mathf.Abs(islandToPlayer.x) > Mathf.Abs(islandToPlayer.y))
                collision.transform.position = new Vector3(collision.transform.position.x, transform.position.y + (Mathf.Sign(islandToPlayer.y) * transform.localScale.y / 2), -3);
            else
                collision.transform.position = new Vector3(transform.position.x + (Mathf.Sign(islandToPlayer.x) * transform.localScale.x / 2), collision.transform.position.y, -3);
            if (timer >= damageTime)
            {
                collision.GetComponent<scr_hpsystem>().takeDamage(1);
                timer = 0.0f;
            }
        }
    }

    protected override void MoveToStart()
    {
        height = Random.Range(
                scr_utilities.GetEdge(edgeId.Bottom, true),
                scr_utilities.GetEdge(edgeId.Top, true));
        transform.position = new Vector3(scr_utilities.GetEdge(startSide, true) + Random.Range(-0.01f, 0.01f), height, transform.position.z);
        transform.localScale = new Vector3(Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f), 0);
        if(crystal)
            Destroy(crystal);
        if(scr_cloudcontroller.instance.GetCrystal())
        {
            crystal = Instantiate(scr_cloudcontroller.instance.crystalRef, transform);
            crystal.transform.localPosition = new Vector3(0, 0.3f, 0);
        }
    }
}
