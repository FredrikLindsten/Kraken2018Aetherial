using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {
    public float shakeTimer;
    public float shakePower;
    Vector3 oldPosition;

    // Use this for initialization
    void Start () {
        oldPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (shakeTimer >= 0)
        {
            Vector2 shakePos = Random.insideUnitCircle * shakePower;

            transform.position = new Vector3(transform.position.x + shakePos.x, transform.position.y + shakePos.y, transform.position.z);

            shakeTimer -= Time.deltaTime;
        } else
        {
            transform.position = oldPosition;
        }
	}

    public void shakeCamera(float shakeAmount, float shakeTime)
    {
        shakePower = shakeAmount;
        shakeTimer = shakeTime;

    }
}
