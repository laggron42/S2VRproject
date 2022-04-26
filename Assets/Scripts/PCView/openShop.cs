using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openShop : MonoBehaviour
{
    public GameObject shop;

    public void shopOpener()
    {
        Debug.Log("shopOpener");
        Animator shopAnim = shop.GetComponent<Animator>();
        if (shopAnim != null)
        {
            bool isShopOpen = shopAnim.GetBool("isShopOpen");
            shopAnim.SetBool("isShopOpen", !isShopOpen);
        }
        else{
            Debug.Log("Shop animator is null");
        }
    }
}
