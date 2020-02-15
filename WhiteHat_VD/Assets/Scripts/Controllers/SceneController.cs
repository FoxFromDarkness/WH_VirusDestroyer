using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadScene(bool rootScene, string sceneName, Action nextMethod)
    {
        if(rootScene)
            SceneManager.LoadScene("_RootScene");

        StartCoroutine(LoadSceneAsynchronously(sceneName, nextMethod));
        
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void UnloadAllScenes(bool rootScene)
    {
        Scene[] scenes = SceneManager.GetAllScenes();

        if(rootScene)
            SceneManager.UnloadSceneAsync(0);

        for (int i = 1; i < scenes.Length; i++)
        {
            SceneManager.UnloadSceneAsync(scenes[i]);
        }
    }

    private IEnumerator LoadSceneAsynchronously (string sceneName, Action nextMethod)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        HeadPanelController.Instance.loadingScreen.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.2f);
            HeadPanelController.Instance.loadingScreen.SetSliderValue(operation.progress);
            yield return new WaitForSeconds(0.2f);//null;
        }

        HeadPanelController.Instance.loadingScreen.gameObject.SetActive(false);
        nextMethod();
    }

}
