using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour
{
    public GameObject transparentTower;
    public GameObject transparentWareHouse;
    public OpenShop openShop;

    private int towerPrice = 15;
    private int warehousePrice = 25;

    public void spawnTower()
    {
        if (!openShop.isOpen)
            return;

        if (!Bank.instance.Buy(towerPrice))
            return;

        openShop.shopCloser();
        // just make a tower_transparent spawn
        GameObject t = Instantiate(transparentTower, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }

    public void spawnWareHouse()
    {
        if (!openShop.isOpen)
            return;

        if (!Bank.instance.Buy(warehousePrice))
            return;
        
        openShop.shopCloser();
        // just make a tower_transparent spawn
        GameObject t = Instantiate(transparentWareHouse, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }
}
