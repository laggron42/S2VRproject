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
        Instantiate(tower);
        TilesManager.instance.EnterEditMode();
    }
}
