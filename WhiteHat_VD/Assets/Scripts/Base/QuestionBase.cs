using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBase : MonoBehaviour
{
    public string question { get; set; }
    public string ansA { get; set; }
    public string ansB { get; set; }
    public string ansC { get; set; }
    public string ansD { get; set; }
    public int correctAns { get; set; }

    public QuestionBase(string fileText)
    {
        string[] data = fileText.Split('|');

        question = data[0];
        ansA = data[1];
        ansB = data[2];
        ansC = data[3];

        if (data[4] != "" || data[4] != null)
            ansD = data[4];

        correctAns = int.Parse(data[5]);

    }

    public override string ToString()
    {
        return $"{question} + {ansA} + {ansB} + {ansC} + {ansD}"; 
    }
}
