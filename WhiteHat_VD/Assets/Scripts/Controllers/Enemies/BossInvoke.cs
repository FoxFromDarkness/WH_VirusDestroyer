using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInvoke : MonoBehaviour
{
    [SerializeField] private BossLevel1 bossLevel;
    private bool wasBoss = false;

    public void ActivateBoss()
    {
        if (!wasBoss)
        {
            wasBoss = true;
            bossLevel.ActivateBoss();
        }
    }
}
