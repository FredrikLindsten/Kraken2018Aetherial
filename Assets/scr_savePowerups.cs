using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_savePowerups : MonoBehaviour {

    int powerupCharges = 0;
    static scr_savePowerups instance;

    public void storePower(int powerups)
    {
        powerupCharges = powerups;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == 0)
            powerupCharges = 0;
        else
            for (int i = 0; i < powerupCharges; i++)
                scr_utilities.player.GetComponent<scr_powerup>().gainPowerup();
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
}
