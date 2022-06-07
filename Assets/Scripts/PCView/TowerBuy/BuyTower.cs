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

        Bank.instance.Buy(towerPrice);

        openShop.shopCloser();
        // just make a tower_transparent spawn
        GameObject t = Instantiate(transparentTower, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }

    public void spawnWareHouse()
    {
        if (!openShop.isOpen)
            return;
            
        Bank.instance.Buy(warehousePrice);

        openShop.shopCloser();
        // just make a tower_transparent spawn
        GameObject t = Instantiate(transparentWareHouse, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }
}
