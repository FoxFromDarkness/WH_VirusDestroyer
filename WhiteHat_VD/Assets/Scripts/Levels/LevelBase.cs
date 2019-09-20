using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    public const float DEFAULT_ORTOGRAPHIC_SIZE = 5;
    public bool isAnimation;
    public float speedOfAnim = 0.1f;
    public float ortographicSize = 5;

    private void Update()
    {
        if (isAnimation)
        {
            if (ortographicSize > DEFAULT_ORTOGRAPHIC_SIZE)
            {
                Camera.main.orthographicSize += speedOfAnim * Time.deltaTime;
                if (Camera.main.orthographicSize >= ortographicSize)
                    isAnimation = false;
            }
            else
            {
                Camera.main.orthographicSize -= speedOfAnim * Time.deltaTime;
                if (Camera.main.orthographicSize <= ortographicSize)
                    isAnimation = false;
            }
        }
    }
}
