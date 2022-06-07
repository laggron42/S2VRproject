using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPotato : MonoBehaviour
{
    public int health;
    public int attackPower;
    private int lastHealth;
    public AudioSource hurt;

    void Start()
    {
        lastHealth = health;
    }

    void Update()
    {
        if (health <= 0)
        {
            Bank.instance.AddMoney(attackPower + 1);
            Destroy(gameObject);
        }
        if (lastHealth>health)
        {
            hurt.Play();
            lastHealth = health;
        }
    }
}
