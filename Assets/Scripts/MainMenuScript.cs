using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{

    public string levelScene;
    public string menuScene;

    public void MenuReturn(){
        SceneManager.LoadScene(menuScene);
    }

    public void StartGame(){
        SceneManager.LoadScene(levelScene);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quitting");
    }
}
