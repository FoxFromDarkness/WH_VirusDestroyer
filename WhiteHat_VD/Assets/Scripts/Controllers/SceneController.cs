using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public LoadingScreenController loadingScreen;

    public void LoadScene(string sceneName, Action nextMethod)
    {
        //SceneManager.LoadScene("_RootScene");
        StartCoroutine(LoadSceneAsynchronously(sceneName, nextMethod));
        
    }

    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private IEnumerator LoadSceneAsynchronously (string sceneName, Action nextMethod)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        loadingScreen.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            yield return new WaitForSeconds(0.2f);
            loadingScreen.SetSliderValue(operation.progress);
            yield return new WaitForSeconds(0.2f);//null;
        }

        loadingScreen.gameObject.SetActive(false);
        nextMethod();
    }

}
