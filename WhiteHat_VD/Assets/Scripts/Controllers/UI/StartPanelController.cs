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


    private void Start()
    {
        playPanel.SetActive(false);
        optionPanel.SetActive(false);

        if (!openStartPanel)
            ChangeVisibility(false);
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

    public void SetMusicVolume(float volume)
    {
        GameController.Instance.SetMusicVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        GameController.Instance.SetSfxVolume(volume);
    }

    public void Mute(bool value)
    {
        GameController.Instance.Mute(value);
    }

    public void CloseSubPanels()
    {
        playPanel.SetActive(false);
        optionPanel.SetActive(false);
    }
}
