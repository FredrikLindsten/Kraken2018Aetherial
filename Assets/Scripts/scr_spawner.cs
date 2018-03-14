using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawner : MonoBehaviour {

    //TODO should be static/global
    public enum SpawnerEnum { Skyslug, Tangler, Ray};
    private string[] spawnType = {
        "obj_skyslug",
        "",
        "obj_aether_ray" };
    public SpawnerEnum spawnId;

    public float spawnOffsetRange = 0;
    private bool edgeX = false;

    public float timer = 0;
    public float spawnDelay = 0;
    public int number = 0;
    public bool debugSpawnLocation = false;

    // Use this for initialization
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        StartCoroutine(Spawn());
        if (GetComponentInParent<scr_leviathan>() != null)
        {
            return;
        }

        float[] distancetoedge =
        {
            transform.position.x - scr_utilities.GetEdge(edgeId.Left,false),
            -(transform.position.x - scr_utilities.GetEdge(edgeId.Right,false)),
            transform.position.y - scr_utilities.GetEdge(edgeId.Bottom,false),
            -(transform.position.y - scr_utilities.GetEdge(edgeId.Top,false))
        };

        int smallest = 0;
        for (int i = 1; i < 4; ++i)
        {
            if(distancetoedge[smallest] > distancetoedge[i])
            {
                smallest = i;
            }
        }
        
        if (smallest == 0 || smallest == 1)
        {
            edgeX = false;
            transform.position = new Vector3(scr_utilities.GetEdge((edgeId)smallest, false), transform.position.y, 0);
        }
        if (smallest == 2 || smallest == 3)
        {
            edgeX = true;
            transform.position = new Vector3(transform.position.x, scr_utilities.GetEdge((edgeId)smallest, false), 0);
        }

        if (debugSpawnLocation)
        {
            Debug.Log(transform.position);
            GameObject go = GameObject.Instantiate(Resources.Load("debugIcon"), transform.position, Quaternion.identity) as GameObject;
            go.transform.localScale = new Vector3(2, 2, 1);
        }
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < number; ++i)
        {
            Vector3 offset = new Vector3(0,0,0);
            if (edgeX)
                offset.x = Random.Range(-spawnOffsetRange, spawnOffsetRange);
            else
                offset.y = Random.Range(-spawnOffsetRange, spawnOffsetRange);

            Instantiate(Resources.Load(spawnType[(int)spawnId]), transform.position + offset, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        Destroy(this);
    }
}
