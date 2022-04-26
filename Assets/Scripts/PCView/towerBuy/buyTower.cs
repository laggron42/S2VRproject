using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyTower : MonoBehaviour
{
    public GameObject tower;
    public openShop openShop;

    public void spawnTower()
    {
        openShop.shopOpener();
        // just make a tower_transparent spawn
        Instantiate(tower, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }
}
