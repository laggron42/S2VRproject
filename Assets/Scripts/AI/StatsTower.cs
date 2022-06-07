using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StatsTower : Stats
{
    TeleportArea teleport;

    void Start()
    {
        teleport = GetComponentInChildren<TeleportArea>();
    }
    public int health;

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
        teleport.locked = true;
    }

    public override void Repair()
    {
        base.Repair();
        if (health == 0)
            teleport.locked = false;
    }
}
