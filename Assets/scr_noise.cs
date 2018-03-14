using UnityEngine;
using System.Collections;

public class scr_noise : MonoBehaviour
{
    public float scale = 10f;
    public float brightness = 0.8f;
    public float fadeInTime = 10f;
    private float fade = 0;
    public float GetFade() { return fade; }
    float movement = 0;

    public int pixelsX;
    public int pixelsY;
    private Texture2D maskTexture;
    private Color[] maskData;
    private Renderer rend;
    private scr_stormcloud cloud;

    void Start()
    {
        rend = GetComponent<Renderer>();
        cloud = GetComponentInParent<scr_stormcloud>();
        maskTexture = new Texture2D(pixelsX, pixelsY);
        maskData = new Color[pixelsX * pixelsY];
        rend.material.mainTexture = maskTexture;
        StartCoroutine(FadeProcess());
    }

    void Update()
    {
        float speed = scr_cloud.GetSpeed()/4;
        movement -= Time.deltaTime * speed;
    }

    IEnumerator FadeProcess()
    {
        while(fade < 1f)
        {
            fade += Time.deltaTime / fadeInTime;
            Fade(fade);
            yield return null;
        }
        while(cloud.wait)
            yield return null;
        while (fade > 0f)
        {
            fade -= Time.deltaTime / fadeInTime;
            Fade(fade);
            yield return null;
        }
    }

    void Fade(float fade)
    {
        rend.material.color = new Color(brightness, brightness, brightness, fade);
        for (int y = 0; y < pixelsY; ++y)
        {
            for (int x = 0; x < pixelsX; ++x)
            {
                float sample =
                    Mathf.PerlinNoise(
                        (float)x / pixelsX * scale * 0.25f * transform.localScale.x + movement, 
                        (float)y / pixelsY * scale * transform.localScale.z);
                sample = Mathf.Clamp01(sample + fade) / 2;
                maskData[y * maskTexture.width + x] = new Color(sample, sample, sample, sample * 2);
            }
        }
        maskTexture.SetPixels(maskData);
        maskTexture.Apply();
    }
}