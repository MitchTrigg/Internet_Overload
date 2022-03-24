using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSceneScript : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;

    public void level1(){
        SceneManager.LoadScene(firstLevel);
    }
    public void level2(){
        SceneManager.LoadScene(secondLevel);
    }
}
