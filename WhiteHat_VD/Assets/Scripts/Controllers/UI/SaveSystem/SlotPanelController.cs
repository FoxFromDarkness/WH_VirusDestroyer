using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotPanelController : PanelBase
{
    public enum SlotPanelMode
    {
        NEW_GAME,
        LOAD_GAME,
        DELETE_GAME
    }
    [SerializeField] private SaveSlotController saveSlotBase;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject inputPanel;
    [HideInInspector] public List<SaveSlotController> listOfSaveSlots;
    private SaveSlotController copyOfSaveSlot;
    private SlotPanelMode _panelMode;
    
    private void OnEnable()
    {
        inputPanel.SetActive(false);
        SetPanelMode(0);
    }

    public void SetPanelMode(int mode)
    {
        this.gameObject.SetActive(true);

        switch (mode)
        {
            case 0:
                this._panelMode = SlotPanelMode.NEW_GAME;
                break;
            case 1:
                this._panelMode = SlotPanelMode.LOAD_GAME;
                break;
            case 2:
                this._panelMode = SlotPanelMode.DELETE_GAME;
                break;
            default:
                break;
        }
        CheckPanelMode();
    }

    private void CheckPanelMode()
    {
        ClearSaveSlots();
        scrollRect.normalizedPosition = Vector2.one;

        var directoryInfo = Directory.GetDirectories(CONSTANS.SAVES_PATH);

        switch (_panelMode)
        {
            case SlotPanelMode.NEW_GAME:
                SetTextGameMode("New Game");
                listOfSaveSlots.Add(InitSaveSlotController(directoryInfo.Length, null));
                break;
            case SlotPanelMode.LOAD_GAME:
                SetTextGameMode("Load Game");
                for (int i = 0; i < directoryInfo.Length; i++)
                    listOfSaveSlots.Add(InitSaveSlotController(i, directoryInfo[i]));
                break;
            case SlotPanelMode.DELETE_GAME:
                SetTextGameMode("Delete Game");
                for (int i = 0; i < directoryInfo.Length; i++)
                    listOfSaveSlots.Add(InitSaveSlotController(i, directoryInfo[i]));
                break;
            default:
                break;
        }
    }

    private void SetTextGameMode(string text)
    {
        textMesh.text = text;
    }

    private void ClearSaveSlots()
    {
        foreach (var item in listOfSaveSlots)
            Destroy(item.gameObject);

        listOfSaveSlots.Clear();
    }

    private SaveSlotController InitSaveSlotController(int idx, string saveDirectoryPath)
    {
        copyOfSaveSlot = Instantiate(saveSlotBase, saveSlotBase.transform.parent);
        copyOfSaveSlot.saveIdx = idx;
        copyOfSaveSlot.gameObject.SetActive(true);
        copyOfSaveSlot.SetSaveDirectory(saveDirectoryPath);
        return copyOfSaveSlot;
    }

    public void OnSaveSlotClick()
    {
        switch (_panelMode)
        {
            case SlotPanelMode.NEW_GAME:
                NewGameMode();
                break;
            case SlotPanelMode.LOAD_GAME:
                LoadGameMode();
                break;
            case SlotPanelMode.DELETE_GAME:
                DeleteGameMode();
                break;
            default:
                break;
        }
    }

    private void NewGameMode()
    {
        inputPanel.SetActive(true);
    }

    private void LoadGameMode()
    {
        GameController.Instance.StartNewGame(true);
    }

    private void DeleteGameMode()
    {
        SaveController.Instance.DeleteFile();
        SetPanelMode(2);
    }
}
