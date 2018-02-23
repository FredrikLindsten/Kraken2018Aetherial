using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_raybolt : MonoBehaviour {

    public float duration;

	// Use this for initialization
	void Start ()
    {
        LineRenderer line = gameObject.GetComponent<LineRenderer>();
        line.SetPosition(0, gameObject.transform.parent.transform.position);
        line.SetPosition(1, scr_utilities.player.transform.position);

        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
