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
        brokenPrefab.SetActive(true);
        repairedPrefab.SetActive(false);
        gameObject.tag = "Untagged";
    }

    public override void Repair()
    {
        brokenPrefab.SetActive(true);
        repairedPrefab.SetActive(false);
        gameObject.tag = "Tower";
    }
}
