using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPotato : MonoBehaviour
{
    public int health;
    public int attackPower;
    private int lastHealth;
    public AudioSource hurt;
    private GameObject GameManager;
    public GameObject fries;

    void Start()
    {
        GameManager = GameObject.Find("GameManager");
        lastHealth = health;
    }

    void Update()
    {
        if (health <= 0)
        {
            Bank.instance.AddMoney(attackPower + 1);
            if (gameObject.GetComponent<Valve.VR.InteractionSystem.FireSource>().isBurning) FriesExplosion.BOOM(fries,transform);
            GameManager.GetComponent<SpawnPoint>().minusPotato();
            Destroy(gameObject);
        }
        if (lastHealth>health)
        {
            hurt.Play();
            lastHealth = health;
        }
    }
}
