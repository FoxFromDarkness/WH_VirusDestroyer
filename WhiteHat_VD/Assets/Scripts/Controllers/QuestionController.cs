using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    [HideInInspector]
    public List<QuestionBase> listOfQuestions;

    private void Start()
    {
        listOfQuestions.Clear();

        string path = Application.dataPath + "/StreamingAssets/Questions";

        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles("*.txt");

        foreach (var item in fileInfo)
        {
            listOfQuestions.Add(new QuestionBase(item.OpenText().ReadToEnd()));
        }
    }

    public QuestionBase RandQuestion()
    {
        return listOfQuestions[Random.Range(0, listOfQuestions.Count)];
    }

}
