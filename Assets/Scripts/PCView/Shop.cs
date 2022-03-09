using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopButton;

    public GameObject TorchButton;
    public GameObject TorchButton1;
    public GameObject TorchButton2;
    public GameObject Torch;
    public GameObject Torch1;
    public GameObject Torch2;

    bool isTorcheActive = true;
    bool isTorcheActive1 = true;
    bool isTorcheActive2 = true;

    // Start is called before the first frame update
    void Start()
    {
        shopPanel.SetActive(false);

        Button btn = shopButton.GetComponent<Button>();
		btn.onClick.AddListener(PanelOpener);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PanelOpener();
        }

        if (Input.GetKey(KeyCode.Escape) && shopPanel.activeSelf)
        {
            PanelOpener();
        }


        TorchButton.GetComponentInChildren<Text>().text = "Yellow Torch: " + !isTorcheActive;
        TorchButton1.GetComponentInChildren<Text>().text = "Blue Torch: " + !isTorcheActive1;
        TorchButton2.GetComponentInChildren<Text>().text = "Green Torch: " + !isTorcheActive2;
    }

    

    void PanelOpener() {  
        if (shopPanel != null) {  
            bool isActive = shopPanel.activeSelf;  
            shopPanel.SetActive(!isActive);
        }

        if (shopPanel.activeSelf)
        {
            Button torchButton = TorchButton.GetComponent<Button>();
            torchButton.onClick.AddListener(TorchOn);
            Button torchButton1 = TorchButton1.GetComponent<Button>();
            torchButton1.onClick.AddListener(TorchOn1);
            Button torchButton2 = TorchButton2.GetComponent<Button>();
            torchButton2.onClick.AddListener(TorchOn2);
        }
    }

    void TorchOn()
    {
        if (Torch != null)
        {
            isTorcheActive = Torch.activeSelf;
            Torch.SetActive(!isTorcheActive);
        }
    }

    void TorchOn1()
    {
        if (Torch1 != null)
        {
            isTorcheActive1 = Torch1.activeSelf;
            Torch1.SetActive(!isTorcheActive1);
        }
    }

    void TorchOn2()
    {
        if (Torch2 != null)
        {
            isTorcheActive2 = Torch2.activeSelf;
            Torch2.SetActive(!isTorcheActive2);
        }
    }
}
