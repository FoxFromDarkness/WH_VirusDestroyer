using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    public float ortographicSize = 5;

    protected virtual void Start()
    {
        Camera.main.orthographicSize = ortographicSize;
    }
}
