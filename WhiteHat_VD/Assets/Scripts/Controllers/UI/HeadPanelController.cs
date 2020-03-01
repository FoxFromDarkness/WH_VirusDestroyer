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

    
    private AudioSource audioSource;
    [Header("UI Sound")]
    [SerializeField] private AudioClip[] uiSFXs;

    private void Start()
    {
        if (Instance == null)
            Instance = this.GetComponent<HeadPanelController>();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUISFX(bool isOk)
    {
        if (isOk) audioSource.clip = uiSFXs[0];
        else audioSource.clip = uiSFXs[1];

        audioSource.Play();
    }
}
