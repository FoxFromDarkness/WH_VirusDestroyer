using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPanelController : MonoBehaviour
{
    public static HeadPanelController Instance;

    public UIPanelController uiPanel;
    public QuestionPanelController questionPanel;
    public SavePlacePanelController savePlacePanel;
    public StartPanelController startPanel;
    public LoadingScreenController loadingScreen;
    public BinaryGameController binaryGame;

    private void Start()
    {
        if (Instance == null)
            Instance = this.GetComponent<HeadPanelController>();
    }
}
