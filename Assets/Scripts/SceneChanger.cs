using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SartGame(){
        SceneManager.LoadScene(1);
    }

    public void Restart(){
        SceneManager.LoadScene(1);
    }

    public void Credits(){
        SceneManager.LoadScene(2);
    }

    public void HowToPlay(){
        SceneManager.LoadScene(3);
    }
    
    public void Options(){
        SceneManager.LoadScene(4);
    }

    public void ReturnMain(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
