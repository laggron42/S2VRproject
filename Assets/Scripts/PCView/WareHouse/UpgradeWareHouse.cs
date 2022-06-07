using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWareHouse : MonoBehaviour
{
    [Tooltip("The Title of the pannel")]
    public Text WareHouseTitle;

    [Tooltip("Heart bar of the WareHouse")]
    public List<RawImage> Hearts;
    [Tooltip("Money bar of the WareHouse")]
    public List<RawImage> Money;

    public Texture FullHeart;
    public Texture EmptyHeart;
    public Texture FullMoney;
    public Texture EmptyMoney;


    [Tooltip("Add Torch button")]
    public Button addTorch;
    [Tooltip("Repair Torch button")]
    public Button repairWareHouse;

    [Tooltip("The list of all WareHouses.")]
    public List<GameObject> WareHouses;

    [Tooltip("The index of the WareHouse selected Button. Should be less than the total number of WareHouse.")]
    public int index = 0;

    [Tooltip("Camera Script of the PC view")]
    public CameraMovement PCview;

    public TowerSelector towerSelector;


    private int repairPrice = 10;
    private int upgradePrice = 20;
    
    private void Start()
    {
        PCview = GameObject.FindObjectOfType<CameraMovement>();
    }

    public void UpdateScreen()
    {
        WareHouseTitle.fontSize = 200;
        WareHouseTitle.text = "WareHouse " + (index);        
        LifeBar();
        moveCam();
        MoneyBar();
        repairWareHouse.interactable = Bank.instance.CurrentMoney >= repairPrice;
        addTorch.interactable = Bank.instance.CurrentMoney >= upgradePrice;
    }

    public void nextbutton()
    {
        index = ++index < WareHouses.Count ? index : 0;
        UpdateScreen();
    }
    public void prevbutton()
    {
        index = index == 0 ? WareHouses.Count - 1 : --index;
        UpdateScreen();
    }


    private void LifeBar()
    {
        int life = WareHouses[index].GetComponent<StatsTower>().health / 10;
        if (life < 0)
            life = 0;
        for (int i = 0; i < 10; i++)
            Hearts[i].texture = life > i ? FullHeart : EmptyHeart;
    }

    private void MoneyBar()
    {
        WareHouse warehouse = WareHouses[index].GetComponent<WareHouse>();
        int money = (int) (((float) warehouse.CurrentMoney / warehouse.MaxCapacity) * 10);
        if (money < 0)
            money = 0;
        for (int i = 0; i < 10; i++)
            Money[i].texture = money > i ? FullMoney : EmptyMoney;


    }

    private void moveCam()
    {
        PCview.LookAt(WareHouses[index].transform.position);
    }

    public void repair()
    {
        Bank.instance.Buy(repairPrice);
        if (WareHouses[index].GetComponent<StatsTower>().health > 99)
            createError("Max life");
        WareHouses[index].GetComponent<StatsTower>().health += 10;
        if (WareHouses[index].GetComponent<StatsTower>().health > 100)
            WareHouses[index].GetComponent<StatsTower>().health = 100;
    }


    public void AddTorch()
    {
        Bank.instance.Buy(upgradePrice);
        WareHouses[index].GetComponent<WareHouse>().Upgrade();
        TorchList torchLists = WareHouses[index].gameObject.GetComponentInChildren<TorchList>(false);
        int i = 0;
        while (torchLists.torches[i].gameObject.activeInHierarchy)
        {
            i++;
        }
        createError("No torch left");

        if (i < torchLists.torches.Count)
            torchLists.torches[i].gameObject.SetActive(true);
        else
            createError("No torch left");
    }

    private void createError(string text)
    {
        // pour afficher une erreur de placement
        string old = addTorch.GetComponentInChildren<Text>().text;
        addTorch.GetComponentInChildren<Text>().text = text;
        StartCoroutine(waitForError());
        addTorch.GetComponentInChildren<Text>().text = old;
    }

    IEnumerator waitForError()
    {
        yield return new WaitForSeconds(2);
    }


}