using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadScene(string levelName){
        StartCoroutine(LoadSceneWithTransition(levelName));
    }

    public void FinishGame(){
        StartCoroutine(LoadSceneWithTransition("GameOver"));
    }

    public void QuitApplication(){
        Application.Quit();
    }

    IEnumerator LoadSceneWithTransition(string levelName){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
