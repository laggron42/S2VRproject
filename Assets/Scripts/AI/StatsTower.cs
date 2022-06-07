using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StatsTower : Stats
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
    }

    public override void Repair()
    {
        base.Repair();
    }
}
