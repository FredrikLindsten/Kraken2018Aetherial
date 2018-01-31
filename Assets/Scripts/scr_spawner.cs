using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawner : MonoBehaviour {

    public enum SpawnerEnum { Skyslug, Tangler, Ray};
    private string[] spawnType = {
        "obj_skyslug",
        "",
        "" };
    public SpawnerEnum spawnId;

    public enum Position { Top, Bottom, Left, Right };
    private Vector3[] spawnPos = {
        new Vector3(0, 5),
        new Vector3(0, -5),
        new Vector3(-9, 0),
        new Vector3(9, 0) };
    public Position spawnPosition;

    public float timer = 0;
    public float spawnDelay = 0;
    public float number = 0;
    public bool debugSpawnLocation = false;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().enabled = false;
        if (debugSpawnLocation)
        {
            GameObject go = GameObject.Instantiate(Resources.Load("debugIcon"), spawnPos[(int)spawnPosition], Quaternion.identity) as GameObject;
            go.transform.localScale = new Vector3(2, 2, 1);
        }
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < number; ++i)
        {
            Instantiate(Resources.Load(spawnType[(int)spawnId]), spawnPos[(int)spawnPosition], Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update () {
	}
}
