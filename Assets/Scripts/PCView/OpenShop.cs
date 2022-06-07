using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;
    public bool isOpen = false;

    public GameObject GeneralPanel;
    public Button buyWareHouse;
    public Button buyTower;
    public GameObject TowerPanel;
    public GameObject WareHousePanel;
    private Animator shopAnim;

    public Text Money;

    private int GeneralPanelActive = 0;
    private UpgradeTower TowerManager;
    private UpgradeWareHouse WareHouseManager;

    private int towerPrice = 15;
    private int warehousePrice = 25;

    Bank bank;

    private void Start() {
        shopAnim = shop.GetComponent<Animator>();
        TowerManager = TowerPanel.GetComponent<UpgradeTower>();
        WareHouseManager = WareHousePanel.GetComponent<UpgradeWareHouse>();
        bank = Bank.instance;
    }

    private void Update()
    {
        if (!isOpen)
            return;
        if (GeneralPanelActive == 0)
            UpdateScreen();
        if (GeneralPanelActive == 1)
            TowerManager.UpdateScreen();
        if (GeneralPanelActive == 2)
            WareHouseManager.UpdateScreen();
    }

    private void UpdateScreen()
    {
        Money.text = "Money: " + bank.CurrentMoney;
        buyTower.interactable = bank.CurrentMoney >= towerPrice;
        Text text = buyTower.GetComponentsInChildren<Text>(true).First(t => t.name == "Cost");
        text.text = towerPrice.ToString();
        buyWareHouse.interactable = bank.CurrentMoney >= warehousePrice;
        text = buyWareHouse.GetComponentsInChildren<Text>(true).First(t => t.name == "Cost");
        text.text = warehousePrice.ToString();
    }

    public void shopOpener()
    {
        if (shopAnim != null)
        {
            isOpen = !isOpen;
            shopAnim.SetBool("isShopOpen", isOpen);
        }
    }

    public void shopCloser()
    {
        if (shopAnim != null)
        {
            isOpen = false;
            shopAnim.SetBool("isShopOpen", isOpen);
        }
    }

    public void menu(int choice)
    {
        GeneralPanel.SetActive(choice == 0);
        TowerPanel.SetActive(choice == 1);
        WareHousePanel.SetActive(choice == 2);
        GeneralPanelActive = choice;
    }
}
