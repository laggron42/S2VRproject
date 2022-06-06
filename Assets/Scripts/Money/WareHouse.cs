using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouse : MonoBehaviour
{
    private int currentMoney;
    private int maxMoney = 100;

    public int MaxCapacity => maxMoney;
    public int CurrentMoney => currentMoney;

    void Start() 
    {
        Bank.instance.AddWareHouse(this);
    }

    // Adds money to the warehouse
    // Returns the amount that couldn't be stored
    public int AddMoney(int amount)
    {
        if (currentMoney + amount > maxMoney)
        {
            currentMoney = maxMoney;
            return (currentMoney + amount) % maxMoney;
        }

        currentMoney += amount;
        return 0;
    }

    // Removes money from the warehouse
    // Returns the amount left to remove
    public int RemoveMoney(int amount)
    {
        if (currentMoney < amount)
        {
            amount = amount - currentMoney;
            currentMoney = 0;
            return amount;
        }

        currentMoney -= amount;
        return 0;
    }

    // Destroy the warehouse
    // Removes its money from the bank
    public void Destroy()
    {
        Bank.instance.RemoveWareHouse(this);
        Destroy(gameObject);
    }

    // Upgrade the warehouse's max capacity
    public void Upgrade()
    {
        maxMoney += 20;
    }
}
