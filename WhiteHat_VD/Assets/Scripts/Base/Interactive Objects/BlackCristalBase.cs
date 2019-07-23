using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCristalBase : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Transform>().Rotate(new Vector3(0, 0, 1.0f));
    }
}
