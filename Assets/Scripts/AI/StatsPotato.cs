using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPotato : MonoBehaviour
{
    public int health;
    public int attackPower;

    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }
}