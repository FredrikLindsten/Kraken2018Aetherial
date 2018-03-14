using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_cloudcontroller : MonoBehaviour {

    public float speed = 0;
    private int cloudCount = 0;
    private int islandCount = 0;
    public GameObject islandRef;
    public GameObject cloudRef;
    public GameObject crystalRef;

    public List<float> crystalSpawnTimers;

    public Sprite islandArt;
    public Sprite cloudArt;
    public bool crystalArt;
    public static scr_cloudcontroller instance = null;

    private List<GameObject> cloudsBg = new List<GameObject>();
    private List<GameObject> islands = new List<GameObject>();

    private void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    public void SetCloudCover(int val)
    {
        int diff = val - cloudCount;
        cloudCount = val;
        for (int i = 0; i < diff; ++i)
        {
            //spawn clouds
            float distance = (scr_utilities.screenWidth + (2 * scr_utilities.padding));
            cloudsBg.Add(Instantiate(cloudRef, new Vector3(distance * (1 + (float)i / diff) - scr_utilities.GetEdge(edgeId.Right, false), 0), Quaternion.identity));
            cloudsBg[cloudsBg.Count - 1].GetComponent<SpriteRenderer>().sprite = cloudArt;
        }
        while (cloudsBg.Count > cloudCount)
        {
            //mark clouds for destruction
            int pickedCloud = Random.Range(0, cloudsBg.Count - 1);
            cloudsBg[pickedCloud].GetComponent<scr_cloud>().MarkForRemoval();
            cloudsBg.RemoveAt(pickedCloud);
        }
    }

    public void SetIslandCount(int val)
    {
        int diff = val - islandCount;
        islandCount = val;
        for (int i = 0; i < diff; ++i)
        {
            //spawn islands
            float distance = (scr_utilities.screenWidth + (2 * scr_utilities.padding));
            islands.Add(Instantiate(islandRef, new Vector3(distance * (1 + (float)i / diff) - scr_utilities.GetEdge(edgeId.Right,false), 0), Quaternion.identity));
            islands[islands.Count - 1].GetComponent<SpriteRenderer>().sprite = islandArt;
        }
        while (islands.Count > islandCount)
        {
            //mark islands for destruction
            int pickedIsland = Random.Range(0, islands.Count - 1);
            islands[pickedIsland].GetComponent<scr_cloud>().MarkForRemoval();
            islands.RemoveAt(pickedIsland);
        }
    }

    // Use this for initialization
    void Start () {
        scr_cloud.SetSpeed(speed);
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    public bool GetCrystal()
    {
        for (int i = 0; i < crystalSpawnTimers.Count; ++i)
        {
            if (crystalSpawnTimers[i] < Time.timeSinceLevelLoad)
            {
                crystalSpawnTimers.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
}
