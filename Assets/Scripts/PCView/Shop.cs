using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopButton;
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
    }

    void PanelOpener() {  
        if (shopPanel != null) {  
            bool isActive = shopPanel.activeSelf;  
            shopPanel.SetActive(!isActive);  
        }  
    }
}
