using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenShop : MonoBehaviour
{
    public GameObject shop;
    public bool isOpen = false;

    public GameObject GeneralPanel;
    public GameObject TowerPanel;
    public GameObject WareHousePanel;
    private Animator shopAnim;

    private bool GeneralPanelActive = false;
    private UpgradeTower upgradeManager;

    private void Start() {
        shopAnim = shop.GetComponent<Animator>();
        upgradeManager = TowerPanel.GetComponent<UpgradeTower>();
    }

    private void Update()
    {
        if (GeneralPanelActive)
            return;

        upgradeManager.UpdateScreen();
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
        GeneralPanelActive = choice != 1;
    }
}
