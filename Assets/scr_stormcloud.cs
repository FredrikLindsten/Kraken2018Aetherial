using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_stormcloud : MonoBehaviour {
    
    public bool wait = false;
    private float movement = 0;
    private int detailMapId;
    private Renderer rend;
    private scr_noise noise;

    [Range(0f,1f)] public float sightLossPoint;

    private bool active = false;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this);
        rend = GetComponentInChildren<Renderer>();
        detailMapId = Shader.PropertyToID("_DetailAlbedoMap");
        gameObject.GetComponentInChildren<Renderer>().material.SetTextureScale(detailMapId, new Vector2(0.25f, 1));
        noise = GetComponentInChildren<scr_noise>();
        active = false;
        StartCoroutine(Move());
	}

    void Update()
    {
        movement -= Time.deltaTime;
        rend.material.SetTextureOffset(detailMapId, new Vector2(movement * scr_cloud.GetSpeed() / 80, 0));
    }

    IEnumerator Move()
    {
        wait = true;
        yield return new WaitForSeconds(noise.fadeInTime * sightLossPoint);
        active = true;
        yield return new WaitForSeconds(noise.fadeInTime * (1 - sightLossPoint));
        while (wait)
            yield return null;
        yield return new WaitForSeconds(noise.fadeInTime * (1 - sightLossPoint));
        active = false;
        yield return new WaitForSeconds(noise.fadeInTime * sightLossPoint);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(LoseSight(other.GetComponent<scr_hpsystem>()));
        }
    }

    IEnumerator LoseSight(scr_hpsystem obj)
    {
        while (!active)
            yield return null;
        obj.LoseSight();
        while (active)
            yield return null;
        obj.GainSight();
        obj.GetComponent<Renderer>().material.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        float fade = noise.GetFade();
        other.GetComponent<Renderer>().material.color = new Color(
            1 - fade,
            1 - fade,
            1 - fade,
            Mathf.Clamp01(1.5f - ((fade - 0.5f) * 2)));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
