using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyWareHouse : MonoBehaviour
{
    public GameObject transparentWareHouse;
    public OpenShop openShop;

    public void spawnWareHouse()
    {
        if (!openShop.isOpen)
            return;
        
        openShop.shopCloser();
        GameObject t = Instantiate(transparentWareHouse, new Vector3(1.5f, 0, -1.5f), Quaternion.identity);
        TilesManager.instance.EnterEditMode();
    }
}
