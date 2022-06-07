using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWarehouse : Stats
{
    void Update()
    {
        if (health <= 0)
        {
            Destroy();
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        Bank.instance.RemoveWareHouse(GetComponent<WareHouse>());
    }

    public override void Repair()
    {
        base.Repair();
        if (health == 0)
            Bank.instance.AddWareHouse(GetComponent<WareHouse>());
    }
}
