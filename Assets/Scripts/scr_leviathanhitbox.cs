using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_leviathanhitbox : scr_hpsystem {

    public GameObject powerup = null;

    protected override void Die()
    {
        GetComponentInParent<scr_leviathan>().Leave();
        GetComponentInParent<scr_leviathan>().takeDamage(2);
        Instantiate(powerup, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
