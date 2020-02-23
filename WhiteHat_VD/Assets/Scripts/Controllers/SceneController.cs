using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadScene(bool rootScene, string sceneName, Action nextMethod)
    {
        if (rootScene)
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

    private IEnumerator LoadUIScene()
    {
        yield return null;
        SceneManager.LoadScene("_UI", LoadSceneMode.Additive);
    }

    private IEnumerator LoadSceneAsynchronously (string sceneName, Action nextMethod)
    {
        yield return StartCoroutine(LoadUIScene());

        yield return new WaitForSeconds(0.3f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        HeadPanelController.Instance.startPanel.gameObject.SetActive(false);
        HeadPanelController.Instance.loadingScreen.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.3f);
            HeadPanelController.Instance.loadingScreen.SetSliderValue(operation.progress);
            yield return new WaitForSeconds(0.3f);//null;
        }

        
        HeadPanelController.Instance.loadingScreen.gameObject.SetActive(false);
        nextMethod();
    }

}
