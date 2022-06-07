using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{

    // Singleton syntax
    // Bank object can be obtained by writting `Bank bank = Bank.instance;`
    public static Bank instance;

    private List<WareHouse> warehouses;
    private int current = 0; // current warehouse

    private int currentMoney = 0;

    public int CurrentMoney => currentMoney;

    private void Awake() 
    {
        Bank.instance = this;

        warehouses = new List<WareHouse>();
        current = 0;
        currentMoney = 20; // Default value
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
        while (value != 0 && current < warehouses.Count)
        {
            value = warehouses[current++].AddMoney(value);
        }
    }

    // Removes money from the warehouse
    // Returns false if not enough money is stored
    public bool Buy(int price)
    {
        while (price != 0 && current >= 0)
        {
            price = warehouses[current--].RemoveMoney(price);
        }
        return price == 0;
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
