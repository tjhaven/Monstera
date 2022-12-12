using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (paused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume(){
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        paused = false;;
    }

    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        //Debug.Log("Menu timeeeeeee");
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        //Debug.Log("Fine be that way");
        Application.Quit();
    }
}
