using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class scr_restart : MonoBehaviour {
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
