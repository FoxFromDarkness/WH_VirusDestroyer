using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public List<PortalController> portals;

    public Vector2 GetPortalPosition(int idx) {
        return portals[idx].transform.position;
    }
}
