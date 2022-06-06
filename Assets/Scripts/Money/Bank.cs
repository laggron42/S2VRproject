using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{

#region Singleton

    public static Bank instance;

    private void Awake() 
    {
        Bank.instance = this;
    }
#endregion

    private int maxAmount = 100;
    private int currentAmount = 50;

    public int CurrentMoney => currentAmount;


    void Start()
    {
        // Starting funds
        currentAmount = 50;
    }
    
    void Update() 
    {
        // TODO: Load game over screen when maxAmount <= 0 (no more warehouses)
    }

    // Updates the bank maxCapacity
    // Set negative value for a decrease in capacity
    void UpdateCapacity(int increase)
    {
        maxAmount += increase;
    }

    // Remove money from the bank if possible
    // Returns true if the operation was successfull
    bool Buy(int price)
    {
        if (currentAmount < price)
            return false;

        currentAmount -= price;
        return true;
    }

    // Add money to the current amount
    // Will not increase if bank reached max capacity
    void Gain(uint amount)
    {
        currentAmount += (int)amount;
        if (currentAmount > maxAmount)
            currentAmount = maxAmount;
    }
}
