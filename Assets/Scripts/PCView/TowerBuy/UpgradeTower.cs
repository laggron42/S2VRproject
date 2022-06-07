using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    [Tooltip("The Title of the pannel")]
    public Text TowerTitle;

    [Tooltip("The Money amount text")]
    public Text MoneyTitle;

    [Tooltip("Heart bar of the tower")]
    public List<RawImage> Hearts;

    public Texture FullHeart;
    public Texture EmptyHeart;


    [Tooltip("Add Torch button")]
    public Button addTorch;
    [Tooltip("Repair Torch button")]
    public Button repairTower;

    [Tooltip("The list of all towers.")]
    public List<GameObject> Towers;

    [Tooltip("The index of the Tower selected Button. Should be less than the total number of Tower.")]
    public int index = 0;

    [Tooltip("Camera Script of the PC view")]
    public CameraMovement PCview;


    public TowerSelector towerSelector;


    private int addTorchPrice = 2;
    private int repairPrice = 10;

    
    private void Start()
    {
        PCview = GameObject.FindObjectOfType<CameraMovement>();
    }

    public void UpdateScreen()
    {
        TowerTitle.text = "Tower " + (index);
        life();
        moveCam();
        MoneyTitle.text = "Money: " + Bank.instance.CurrentMoney;
        addTorch.interactable = Bank.instance.CurrentMoney >= addTorchPrice;
        repairTower.interactable = Bank.instance.CurrentMoney >= repairPrice;
    }

    public void nextbutton()
    {
        index = ++index < Towers.Count ? index : 0;
        UpdateScreen();
    }
    public void prevbutton()
    {
        index = index == 0 ? Towers.Count - 1 : --index;
        UpdateScreen();
    }


    private void life()
    {
        int life = Towers[index].GetComponent<StatsTower>().health / 10;
        if (life < 0)
            life = 0;
        for (int i = 0; i < 10; i++)
            Hearts[i].texture = life > i ? FullHeart : EmptyHeart;
    }

    private void moveCam()
    {
        PCview.LookAt(Towers[index].transform.position);
    }

    public void repair()
    {
        Bank.instance.Buy(repairPrice);
        if (Towers[index].GetComponent<StatsTower>().health > 99)
            createError("Max life");
        Towers[index].GetComponent<StatsTower>().health += 10;
        if (Towers[index].GetComponent<StatsTower>().health > 100)
            Towers[index].GetComponent<StatsTower>().health = 100;
    }


    public void AddTorch()
    {
        Bank.instance.Buy(addTorchPrice);
        TorchList torchLists = Towers[index].gameObject.GetComponentInChildren<TorchList>(false);
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