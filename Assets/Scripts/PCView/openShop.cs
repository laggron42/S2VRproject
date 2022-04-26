using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openShop : MonoBehaviour
{
    public GameObject shop;
    public bool isOpen = false;

    private Animator shopAnim;

    private void Start() {
        shopAnim = shop.GetComponent<Animator>();
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
}
