using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_powerup : MonoBehaviour {
    
    class Ghost
    {
        public Vector3 pos;
        public Vector3 acc;
    }

    public static scr_powerup instance = null;
    private List<Ghost> ghosts = new List<Ghost>();
    public int nrOfGhosts = 0;

    public int powerupCharges = 0;
    public float powerupTime = 0;
    private bool powerOn = false;
    private scr_stormcloud stormcloud;
    public GameObject stormcloudref;
    public Animator powerupIcon;

    public AudioClip useClip;
    private AudioSource audioSource;

    public void gainPowerup()
    {
        powerupCharges++;
    }

    public Vector3 GetGhost(int id)
    {
        return ghosts[id].pos;
    }

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null)
            Destroy(this);
        instance = this;
        for (int i = 0; i < nrOfGhosts; i++)
        {
            ghosts.Add(new Ghost());
        }
	}

    private void ScrambleGhosts()
    {
        for(int i = 0; i < ghosts.Count; ++i)
        {
            ghosts[i].pos = new Vector3(
                Random.Range(
                    scr_utilities.edges[(int)scr_utilities.edgeId.Left], 
                    scr_utilities.edges[(int)scr_utilities.edgeId.Right]),
                Random.Range(
                    scr_utilities.edges[(int)scr_utilities.edgeId.Bottom], 
                    scr_utilities.edges[(int)scr_utilities.edgeId.Top]));
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        powerOn = false;
        stormcloud.speed = scr_cloud.speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (powerupCharges > 0)
            powerupIcon.SetBool("Powerup", true);
        else
            powerupIcon.SetBool("Powerup", false);
        if (Input.GetKeyDown(KeyCode.Q) && powerupCharges > 0)
        {
            //Use powerup
            audioSource.PlayOneShot(useClip);
            powerupCharges--;
            ScrambleGhosts();
            stormcloud = Instantiate(stormcloudref, new Vector3(scr_utilities.screenWidth + (2 * scr_utilities.padding), 0, 1), Quaternion.identity).GetComponent<scr_stormcloud>();
            powerOn = true;
            StartCoroutine(Timer(powerupTime));
            stormcloud.speed = scr_cloud.speed;
        }
        if (powerOn)
        {
            for (int i = 0; i < ghosts.Count; ++i)
            {
                ghosts[i].acc.x += Random.Range(-0.03f, 0.03f);
                ghosts[i].acc.y += Random.Range(-0.03f, 0.03f);
                if (ghosts[i].pos.x < scr_utilities.edges[(int)scr_utilities.edgeId.Left] || 
                    ghosts[i].pos.x > scr_utilities.edges[(int)scr_utilities.edgeId.Right])
                    ghosts[i].acc.x *= -1;
                if (ghosts[i].pos.y < scr_utilities.edges[(int)scr_utilities.edgeId.Bottom] || 
                    ghosts[i].pos.y > scr_utilities.edges[(int)scr_utilities.edgeId.Top])
                    ghosts[i].acc.y *= -1;
                ghosts[i].acc *= 0.9f;
                ghosts[i].pos += ghosts[i].acc;
            }
        }
    }
}
