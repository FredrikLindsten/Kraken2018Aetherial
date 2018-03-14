using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloud : MonoBehaviour {

    private static float speed = 0;
    public float verticalSpeed = 0;
    private float movement = 0;
    private bool remove = false;
    protected float height = 0;

    protected static edgeId startSide = edgeId.Right;
    protected static edgeId endSide = edgeId.Left;

    public static void SetSpeed(float val)
    {
        speed = val;
        if (val < 0)
        {
            startSide = edgeId.Left;
            endSide = edgeId.Right;
        }
        else
        {
            startSide = edgeId.Right;
            endSide = edgeId.Left;
        }
    }

    public static float GetSpeed()
    {
        return speed;
    }

    public void MarkForRemoval()
    {
        remove = true;
    }

	// Use this for initialization
	void Start ()
    {
        height = Random.Range(
                scr_utilities.GetEdge(edgeId.Bottom,true),
                scr_utilities.GetEdge(edgeId.Top,true));
        transform.Translate(0, 0, 2);
    }

    protected virtual void MoveToStart()
    {
        height = Random.Range(
                scr_utilities.GetEdge(edgeId.Bottom, true),
                scr_utilities.GetEdge(edgeId.Top, true));
        transform.position = new Vector3(scr_utilities.GetEdge(startSide,true) + Random.Range(-0.01f,0.01f) + 3, height,transform.position.z);
        transform.localScale = new Vector3(Random.Range(0.6f, 1.4f), Random.Range(0.6f, 1.4f),0);
    }
	
	// Update is called once per frame
	void Update () {
        movement = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x - movement, height + Mathf.Cos(transform.position.x)*verticalSpeed, transform.position.z);

        if (transform.position.x < (scr_utilities.GetEdge(endSide, true)-3))
        {
            if (remove)
                Destroy(this.gameObject);
            else
                MoveToStart();
        }
	}
}
