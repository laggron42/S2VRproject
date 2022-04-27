using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SwitchTowerTeleportType : MonoBehaviour
{
    public GameObject teleportArea;
    public GameObject teleportPoint;
    public TorchList torches; // Ugly but saves performances a lot

    public void SetToAreaMode()
    {
        teleportArea.SetActive(true);
        teleportPoint.SetActive(false);
    }
    public void SetToPointMode()
    {
        teleportArea.SetActive(false);
        teleportPoint.SetActive(true);
    }
}
