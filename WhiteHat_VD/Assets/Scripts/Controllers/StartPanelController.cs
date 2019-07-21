using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelController : PanelBase
{
    [Header("StartPanelController")]
    [SerializeField]
    private bool openStartPanel = true;
    public GameObject playPanel;
    public GameObject optionPanel;

    public GameController gameController;

    private void Start()
    {
        playPanel.SetActive(false);
        optionPanel.SetActive(false);

        if (!openStartPanel)
            gameObject.SetActive(false);

    }

    public void OpenSubPanels(int idx)
    {
        switch (idx)
        {
            case 0:
                playPanel.SetActive(true);
                break;

            case 1:
                optionPanel.SetActive(true);
                break;
        }
    }

    public void CloseSubPanels()
    {
        playPanel.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void PlayGameClick()
    {
        gameController.StartNewGame();
        this.gameObject.SetActive(false);
    }
}
