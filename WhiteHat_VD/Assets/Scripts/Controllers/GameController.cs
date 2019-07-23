using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("PlayGame Options")]
    public GameObject player;

    public void StartNewGame()
    {
        player.SetActive(true);
        player.GetComponent<PlayerBase>().startPosition = new Vector3(-1240.0f, 255.0f);
        GetComponent<SceneController>().LoadScene("GameLevel_TestLevel");
        player.transform.position = player.GetComponent<PlayerBase>().startPosition;
    }
}
