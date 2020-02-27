using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{
    private Camera mainCamera;
    public const float DEFAULT_ORTOGRAPHIC_SIZE = 6.5f;
    [Header("Camera options")]
    public bool isAnimation;
    public float speedOfAnim = 0.1f;
    public float ortographicSize;
    [Space]
    [Header("Background Image options")]
    public Sprite bgSprite;
    public Vector2 bgScale;

    protected virtual void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        mainCamera.orthographicSize = DEFAULT_ORTOGRAPHIC_SIZE;
    }

    private void OnEnable()
    {
        if(bgSprite != null)
        {
            GameController.Instance.SetCurrentBackground(bgSprite, bgScale);
        }
    }
    private void Update()
    {
        if (isAnimation)
        {
            if (ortographicSize > DEFAULT_ORTOGRAPHIC_SIZE)
            {
                mainCamera.orthographicSize += speedOfAnim * Time.deltaTime;
                if (mainCamera.orthographicSize >= ortographicSize)
                    isAnimation = false;
            }
            else
            {
                mainCamera.orthographicSize -= speedOfAnim * Time.deltaTime;
                if (mainCamera.orthographicSize <= ortographicSize)
                    isAnimation = false;
            }
        }
    }

}
