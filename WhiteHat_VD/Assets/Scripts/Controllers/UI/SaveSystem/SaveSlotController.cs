using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SaveSlotController : MonoBehaviour
{
    public int saveIdx;
    [SerializeField]
    private TextMeshProUGUI textMesh;

    //public void SetSaveIndex(int idx)
    //{
    //    SaveController.saveNumber = idx;
    //}

    public bool SetSaveDirectory(string saveDirectoryPath)
    {

        if(Directory.Exists(saveDirectoryPath))
        {
            string[] name = saveDirectoryPath.Split('©');
            textMesh.text = name[1];
            return true;
        }
        else
        {
            textMesh.text = "<Free Slot>";
            return false;
        }
    }

    public void OnSaveSlotClick()
    {
        SaveController.saveNumber = saveIdx;
        SaveController.saveNickName = textMesh.text;
    }
}
