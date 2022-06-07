using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Tutorial : MonoBehaviour
{
    public TMP_Text messageText;
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
        StartCoroutine(RunTutorial());
    }

    void UpdateText(string content)
    {
        messageText.text = content;
    }

    IEnumerator WaitForAction(ISteamVR_Action_Boolean action)
    {
        while (!action.stateUp)
            yield return null;
    }

    IEnumerator RunTutorial()
    {
        yield return PressButton();
        yield return new WaitForSeconds(2);
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
            if (hand != null)
            {
                ControllerButtonHints hints = ControllerButtonHints.GetControllerButtonHints( hand );
                if (hints != null && hints.autoSetWithControllerRangeOfMotion)
                    hand.ResetTemporarySkeletonRangeOfMotion();
            }
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
