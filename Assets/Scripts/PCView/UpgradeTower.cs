using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour
{
    [Tooltip("The Title of the pannel")]
    public Text TowerTitle;
    [Tooltip("Add Torch button")]
    public Button addTorch;
    [Tooltip("Repair Torch button")]
    public Button repairTower;

    [Tooltip("The list of all towers.")]
    public List<GameObject> Towers;

    [Tooltip("The prev Button. To change the selected Tower.")]
    public Button prev;
    [Tooltip("The next Button. To change the selected Tower.")]
    public Button next;

    [Tooltip("The index of the Tower selected4 Button. Should be less than the total number of Tower.")]
    public int index = 0;



    public TowerSelector towerSelector;

    
    private void Start()
    {
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        TowerTitle.text = "Tower " + (index);
    }

    public void nextbutton()
    {
        index = ++index < Towers.Count ? index : 0;
        UpdateScreen();
    }
    public void prevbutton()
    {
        index = index-- < 0 ? index : Towers.Count - 1;
        UpdateScreen();
    }


    public void AddTorch()
    {
        List<Transform> childrens = new List<Transform> (Towers[index].gameObject.GetComponentsInChildren<Transform>(false));

        for (int i = childrens.Count - 1; i >= 0; i--)
        {
            Debug.Log(i);
            if (childrens[i].CompareTag("torch") == false)
                childrens.RemoveAt(i);
        }

        for (int i = 0; i < childrens.Count; i++)
        {
            Debug.Log(i);
            if (childrens[i].gameObject.activeInHierarchy)
                continue;
            childrens[i].gameObject.SetActive(false);
            break;
        }
        createError("No torch left");


    }

    public void createError(string text)
    {
        // pour afficher une erreur de placement
        addTorch.GetComponentInChildren<Text>().text = text;
        StartCoroutine(waitForError());
        addTorch.GetComponentInChildren<Text>().text = "Add a torch";
    }

    IEnumerator waitForError()
    {
        yield return new WaitForSeconds(2);
    }
}