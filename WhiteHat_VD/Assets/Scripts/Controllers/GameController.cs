using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool IsInputEnable { get; set; }

    [Header("PlayGame Options")]
    public GameObject player;

    public void StartNewGame()
    {
        player.SetActive(true);
        player.GetComponent<PlayerBase>().StartPosition = new Vector3(-1240.0f, 255.0f);
        GetComponent<SceneController>().UnloadAllScenes(false);
        GetComponent<SceneController>().LoadScene(false, "GameLevel_TestLevel", SetCharacterPosition);
        IsInputEnable = true;
    }

    private void SetCharacterPosition()
    {
        player.transform.position = player.GetComponent<PlayerBase>().StartPosition;
    }
}
