using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public double healthpoint { get; set; }
    public Vector3 playerStartPosition { get; set; }

    private void Start()
    {
        healthpoint = 0.0;
        playerStartPosition = new Vector3(0, 0);
    }
}
