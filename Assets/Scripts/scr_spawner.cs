using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawner : MonoBehaviour {

    //TODO should be static/global
    public enum SpawnerEnum { Skyslug, Tangler, Ray};
    private string[] spawnType = {
        "obj_skyslug",
        "",
        "" };
    public SpawnerEnum spawnId;

    //TODO move to scr_spawner
    public static float[] edges = new float[4];

    public float spawnOffsetRange = 0;
    private bool edgeX = false;
    private Vector3 spawnPos;

    public float timer = 0;
    public float spawnDelay = 0;
    public float number = 0;
    public bool debugSpawnLocation = false;

    // Use this for initialization
    void Start()
    {
        float[] distancetoedge =
        {
            transform.position.x - edges[0], //left
            -(transform.position.x - edges[1]),//right
            transform.position.y - edges[2],//bottom
            -(transform.position.y - edges[3])//top
        };

        int smallest = 0;
        for (int i =1;i < 4;++i)
        {
            if(distancetoedge[smallest] > distancetoedge[i])
            {
                smallest = i;
            }
        }
        
        if (smallest == 0 || smallest == 1)
        {
            edgeX = false;
            spawnPos.x = edges[smallest];
            spawnPos.y = transform.position.y;
        }
        if (smallest == 2 || smallest == 3)
        {
            edgeX = true;
            spawnPos.x = transform.position.x;
            spawnPos.y = edges[smallest];
        }

        GetComponent<Renderer>().enabled = false;
        if (debugSpawnLocation)
        {
            Debug.Log(spawnPos);
            GameObject go = GameObject.Instantiate(Resources.Load("debugIcon"), spawnPos, Quaternion.identity) as GameObject;
            go.transform.localScale = new Vector3(2, 2, 1);
        }
        StartCoroutine(Spawn());
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

            Instantiate(Resources.Load(spawnType[(int)spawnId]), spawnPos + offset, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update () {
	}
}
