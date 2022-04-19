using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyTower : MonoBehaviour
{
    public GameObject tower;

    public void spawnTower()
    {
        // just make a tower_transparent spawn
        Instantiate(tower);
    }
}
