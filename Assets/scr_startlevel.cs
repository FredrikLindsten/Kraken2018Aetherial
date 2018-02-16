using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_startlevel : MonoBehaviour {

    public GameObject Mainmenu;
    public GameObject Controls;

    public void EnterControls()
    {
        Controls.SetActive(true);
        Mainmenu.SetActive(false);
    }

    public void LeaveControls()
    {
        Controls.SetActive(false);
        Mainmenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene()
    {
       SceneManager.LoadScene("PlaytestScene");
    }
}
