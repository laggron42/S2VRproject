using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start() {
        shopAnim = shop.GetComponent<Animator>();
        TowerManager = TowerPanel.GetComponent<UpgradeTower>();
        WareHouseManager = WareHousePanel.GetComponent<UpgradeWareHouse>();
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
        Money.text = "Money : " + Bank.instance.CurrentMoney;
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
