using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_restart : MonoBehaviour {
    
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
