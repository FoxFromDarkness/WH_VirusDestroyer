using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionController : MonoBehaviour
{
    private static List<QuestionBase> listOfQuestions = new List<QuestionBase>();

    private void Start()
    {
        listOfQuestions.Clear();

        string path = CONSTANS.QUESTIONS_PATH;

        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles("*.txt");

        foreach (var item in fileInfo)
        {
            listOfQuestions.Add(new QuestionBase(item.OpenText().ReadToEnd()));
        }
    }

    public static QuestionBase RandQuestion()
    {
        return listOfQuestions[Random.Range(0, listOfQuestions.Count)];
    }

}
