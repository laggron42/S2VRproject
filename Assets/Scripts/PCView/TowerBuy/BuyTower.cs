using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTower : MonoBehaviour
{
    public GameObject transparentTower;
    public OpenShop openShop;

    public void spawnTower()
    {
        if (!openShop.isOpen)
            return;
        
        openShop.shopCloser();
        // just make a tower_transparent spawn
        GameObject t = Instantiate(transparentTower, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }
}
