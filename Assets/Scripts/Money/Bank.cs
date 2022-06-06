using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{

#region Singleton

    // Singleton syntax
    // Bank object can be obtained by writting `Bank bank = Bank.instance;`
    public static Bank instance;

    private void Awake() 
    {
        Bank.instance = this;
    }
#endregion

    private List<WareHouse> warehouses;
    private int current = 0; // current warehouse

    private int currentMoney = 0;

    public int CurrentMoney => currentMoney;

    void Start()
    {
        warehouses = new List<WareHouse>();
        current = 0;
    }

    // Adds a warehouse to the list
    public void AddWareHouse(WareHouse w)
    {
        warehouses.Add(w);
    }
    
    // Adds money to the current warehouse
    // Will go to the next one if current is full
    public void AddMoney(int value)
    {
        int remainder = warehouses[current].AddMoney(value);
        while (remainder != 0 && current < warehouses.Count)
        {
            remainder = warehouses[++current].AddMoney(remainder);
        }
    }

    // Removes a warehouse from the bank
    // Called when the warehouse is deleted
    public void RemoveWareHouse(WareHouse w)
    {
        warehouses.Remove(w);

        if (current > warehouses.Count)
            current--;
        
        currentMoney -= w.CurrentMoney;
        if (currentMoney < 0)
            currentMoney = 0;
    }
}
