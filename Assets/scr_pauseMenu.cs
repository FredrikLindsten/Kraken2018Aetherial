using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_pauseMenu : MonoBehaviour {

    public void Continue()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void EnterControls()
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }
    public void LeaveControls()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
