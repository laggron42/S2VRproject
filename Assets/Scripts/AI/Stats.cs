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
        brokenPrefab.SetActive(true);
        repairedPrefab.SetActive(false);
        gameObject.tag = "Untagged";
    }

    public virtual void Repair()
    {
        brokenPrefab.SetActive(false);
        repairedPrefab.SetActive(true);
        gameObject.tag = "Tower";
    }
}
