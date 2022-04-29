using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneScript : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;
    public string thirdLevel;
    public string fourthLevel;
    public string fifthLevel;
    public string sixthLevel;
    public string MenuReturn;

    public void level1(){
        SceneManager.LoadScene(firstLevel);
    }
    public void level2(){
        SceneManager.LoadScene(secondLevel);
    }
    public void level3(){
        SceneManager.LoadScene(thirdLevel);
    }
    public void level4(){
        SceneManager.LoadScene(fourthLevel);
    }
    public void level5(){
        SceneManager.LoadScene(fifthLevel);
    }
    public void level6(){
        SceneManager.LoadScene(sixthLevel);
    }
    
    public void MainMenu(){
        SceneManager.LoadScene(MenuReturn);
    }
}
