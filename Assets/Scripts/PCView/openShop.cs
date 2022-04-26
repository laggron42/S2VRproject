using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openShop : MonoBehaviour
{
    public GameObject shop;
    public bool isopen = false;

    public void shopOpener()
    {
        Animator shopAnim = shop.GetComponent<Animator>();
        if (shopAnim != null)
        {
            isopen = !isopen;
            shopAnim.SetBool("isShopOpen", isopen);
        }
    }
}
