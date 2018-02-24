using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloud : MonoBehaviour {

    public static float speed = 0;
    public float verticalSpeed = 0;
    private float movement = 0;
    private bool remove = false;
    private float height = 0;

    public Sprite alt;
    private Sprite def;

    public void MarkForRemoval()
    {
        remove = true;
    }

	// Use this for initialization
	void Start ()
    {
        def = GetComponent<SpriteRenderer>().sprite;
        height = Random.Range(
                scr_utilities.edges[(int)scr_utilities.edgeId.Bottom] - scr_utilities.padding,
                scr_utilities.edges[(int)scr_utilities.edgeId.Top] + scr_utilities.padding);
        transform.Translate(0, 0, 2);
    }

    void MoveToStart()
    {
        height = Random.Range(
                scr_utilities.edges[(int)scr_utilities.edgeId.Bottom] - scr_utilities.padding,
                scr_utilities.edges[(int)scr_utilities.edgeId.Top] + scr_utilities.padding);
        transform.position = new Vector3(scr_utilities.edges[(int)scr_utilities.edgeId.Right] + scr_utilities.padding + Random.Range(-0.01f,0.01f), height,transform.position.z);
        transform.localScale = new Vector3(Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f),0);
        if (Random.value < 0.5f)
            GetComponent<SpriteRenderer>().sprite = alt;
        else
            GetComponent<SpriteRenderer>().sprite = def;

    }
	
	// Update is called once per frame
	void Update () {
        movement = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x - movement, height + Mathf.Cos(transform.position.x)*verticalSpeed, transform.position.z);

        if (transform.position.x < scr_utilities.edges[(int)scr_utilities.edgeId.Left] - scr_utilities.padding)
        {
            if (remove)
                Destroy(this.gameObject);
            else
                MoveToStart();
        }
	}
}
