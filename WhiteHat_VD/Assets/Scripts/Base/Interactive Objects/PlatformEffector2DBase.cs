using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEffector2DBase : MonoBehaviour
{
    [SerializeField] private float time;
    private PlatformEffector2D platformEffector2D;

    private void Start()
    {
        platformEffector2D = GetComponent<PlatformEffector2D>();     
    }

    public void RunPlatformOperation()
    {
        StartCoroutine(CoRunPlatformOperation());
    }

    private IEnumerator CoRunPlatformOperation()
    {
        platformEffector2D.colliderMask = 311;
        yield return new WaitForSeconds(time);
        platformEffector2D.colliderMask = -1;
    }
}
