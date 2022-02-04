using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Tutorial : MonoBehaviour
{
    public Text textCanvas;
    public Teleport teleport;
    public ISteamVR_Action_Boolean menuAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("OpenMenu");
    public ISteamVR_Action_Boolean tpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        teleport.enabled = false;
        Invoke("StartTutorial", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTutorial()
    {
        StartCoroutine(PressButton());
    }

    void UpdateText(string content)
    {
        textCanvas.text = content;
    }

    IEnumerator WaitForAction(ISteamVR_Action_Boolean action)
    {
        while (!action.stateUp)
            yield return null;
    }

    IEnumerator RunTutorial()
    {
        yield return PressButton();
        yield return new WaitForSeconds(5);
        yield return Teleport();
    }

    IEnumerator PressButton()
    {
        UpdateText("Press the button to open the menu");
        foreach (Hand hand in player.hands)
        {
            ControllerButtonHints.HideAllTextHints(hand);
            ControllerButtonHints.ShowTextHint(hand, menuAction, "Open menu");
        }
        yield return StartCoroutine(WaitForAction(menuAction));
        foreach (Hand hand in player.hands)
        {
            ControllerButtonHints.HideAllTextHints(hand);
        }
        UpdateText("Good job!");
    }
    IEnumerator Teleport()
    {
        teleport.enabled = true;
        teleport.ShowTeleportHint();
        UpdateText("Now try to teleport to a zone.");
        yield return StartCoroutine(WaitForAction(tpAction));
        UpdateText("Good job!");
    }
}
