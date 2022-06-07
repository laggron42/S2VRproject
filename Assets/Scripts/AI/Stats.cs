using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Range(0, 100)]
    public int health;

    public GameObject brokenPrefab;
    public GameObject repairedPrefab;

    protected virtual void Destroy()
    {
    }

    public virtual void Repair()
    {
    }
}
