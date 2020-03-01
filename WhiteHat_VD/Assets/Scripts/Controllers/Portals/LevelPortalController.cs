using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortalController : MonoBehaviour
{
    [SerializeField]
    private bool isActive = true;
    public bool isOpenGame;

    public bool IsActive { get { return isActive; } set { isActive = value; SetActive(value); } }
    public string descriptionOpen = "Press 'Up arrow' to enter";
    public string descriptionClosed = "Press 'Up arrow' to unlock portal";
    public Vector2 startLevelPosition;
    public GameController.PlayableWorld thisSceneName;
    public GameController.PlayableWorld nextSceneName;
    [SerializeField] private Transform doorL;
    [SerializeField] private Transform doorR;
    [SerializeField] private Vector2 doorStartPos = new Vector2(0.094f, -0.016f); //for doorL x*=-1
    [SerializeField] private Vector2 doorEndPos = new Vector2(0.19f, -0.016f);


    private void Start()
    {
        //SetActive(isActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            IsActive = true;
        }
    }

    private void SetActive(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
        StartCoroutine(MoveDoor(true, true, doorL));
        StartCoroutine(MoveDoor(true, false, doorR));
    }

    private IEnumerator MoveDoor(bool isActive, bool isLeft, Transform door)
    {
        

        Vector3 endPos = new Vector3(doorEndPos.x * (isLeft == true ? -1 : 1), doorEndPos.y, 0);

        if (isLeft)
        {
            while (door.transform.localPosition.x > endPos.x)
            {
                yield return new WaitForSeconds(0.01f);
                if (isActive)
                    door.Translate(new Vector3(doorEndPos.x * (isLeft == true ? -1 : 1), doorEndPos.y) * Time.deltaTime * 2);
            }
        }
        else
        {
            while (door.transform.localPosition.x < endPos.x)
            {
                yield return new WaitForSeconds(0.01f);
                if (isActive)
                    door.Translate(new Vector3(doorEndPos.x * (isLeft == true ? -1 : 1), doorEndPos.y) * Time.deltaTime * 2);
            }
        }

    }
}
