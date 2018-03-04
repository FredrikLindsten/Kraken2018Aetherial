using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_restart : MonoBehaviour {
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(scr_utilities.leviathan);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(scr_utilities.leviathan);
    }

    public void LoadLevel(int val)
    {
        SceneManager.LoadScene(val);
    }

    public void checkpoint()
    {
        scr_utilities.instance.StopWaiting();
    }
}
