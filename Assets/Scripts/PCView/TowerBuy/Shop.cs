using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text addTorchText;
    public TowerSelector towerSelector;

    void Start()
    {

    }

    public void LigthTorch(int index)
    {
        foreach (SwitchTowerTeleportType tower in towerSelector.towers)
        {
            tower.torches.torches[index].gameObject.SetActive(true);
        }
    }

    public void UnLigthTorch(int index)
    {
        foreach (SwitchTowerTeleportType tower in towerSelector.towers)
        {
            tower.torches.GetComponentsInChildren<Transform>()[index].gameObject.SetActive(true);
        }
    }

    public void RandomTorch()
    {
        if (towerSelector.towers.Count < 0)
        {
            createError("No towers");
            return;
        }
        List<GameObject> torches = towerSelector.towers[0].torches.torches;
        for (int i = 0; i < torches.Count; i++)
        {
            if (torches[i].activeInHierarchy)
                continue;
            LigthTorch(i);
            break;
        }
        createError("No torch left");
    }

    public void createError(string text)
    {
        // pour afficher une erreur de placement
        addTorchText.text = text;
        StartCoroutine(waitForError());
        addTorchText.text = "Add a torch";
    }

    IEnumerator waitForError()
    {
        yield return new WaitForSeconds(2);
    }

}
