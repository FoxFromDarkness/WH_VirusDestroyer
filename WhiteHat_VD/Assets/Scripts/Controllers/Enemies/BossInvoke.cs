using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInvoke : MonoBehaviour
{
    [SerializeField] private BossLevel1 bossLevel;

    public void ActivateBoss()
    {
        bossLevel.ActivateBoss();
    }
}
