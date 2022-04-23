using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    public List<GameObject> ListTorch = new List<GameObject>();


    public void addTorch(Transform torch)
    {
        ListTorch.Add(torch.gameObject);
        unLigthTorch(torch.gameObject);
    }

    public void addTower(GameObject tower)
    {
        // recuperer les torches et les mettre dans la liste
        addTorch(tower.transform.GetChild(0).GetChild(0));
        addTorch(tower.transform.GetChild(0).GetChild(1));
        addTorch(tower.transform.GetChild(0).GetChild(2));
    }

    public void removeTorch(GameObject torch)
    {
        ListTorch.Remove(torch);
    }

    public void ligthTorch(GameObject torch)
    {
        torch.SetActive(true);
    }

    public void unLigthTorch(GameObject torch)
    {
        torch.SetActive(false);
    }

    public void randomTorch()
    {
        int random = Random.Range(0, ListTorch.Count);
        if (random < 0)
            return;
        ligthTorch(ListTorch[random]);
        removeTorch(ListTorch[random]);
    }



}
