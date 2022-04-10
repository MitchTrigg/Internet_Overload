using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneScript : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;
    public string thirdLevel;

    public void level1(){
        SceneManager.LoadScene(firstLevel);
    }
    public void level2(){
        SceneManager.LoadScene(secondLevel);
    }
    public void level3(){
        SceneManager.LoadScene(thirdLevel);
    }
}
