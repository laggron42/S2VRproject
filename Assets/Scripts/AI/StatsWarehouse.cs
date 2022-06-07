using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWarehouse : Stats
{
    WareHouse warehouse;

    void Start()
    {
        warehouse = GetComponent<WareHouse>();
    }

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
        Bank.instance.RemoveWareHouse(warehouse);
    }

    public override void Repair()
    {
        base.Repair();
        if (health == 0)
            Bank.instance.AddWareHouse(warehouse);
    }
}
