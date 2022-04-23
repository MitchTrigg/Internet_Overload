using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public string levelSelect;
    public string mainMenu;
    public bool isPaused;
    public GameObject pauseMenuCanvas;

    void Update(){
        if(isPaused){
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else{
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            isPaused = !isPaused;
        }
    }

    public void Resume(){
        isPaused = false;
    }
    public void LevelSelect(){
        Application.LoadLevel(levelSelect);
    }
    public void QuitMain(){
        Application.LoadLevel(mainMenu);
    }


    /*
  public static bool GameIsPaused = false;
  public GameObject pauseMenuUI;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }
    void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    */
}
