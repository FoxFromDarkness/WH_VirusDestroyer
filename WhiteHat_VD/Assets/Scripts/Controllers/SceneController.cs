using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public LoadingScreenController loadingScreen;

    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene("_RootScene");
        StartCoroutine(LoadSceneAsynchronously(sceneName));
        
    }

    private IEnumerator LoadSceneAsynchronously (string sceneName)
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
    }

}
