using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TeleportTutorial : TutorialStep
{
    public Teleport teleport;
    public ISteamVR_Action_Boolean tpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    private void Start()
    {
        teleport.enabled = false;
    }
    
    public override IEnumerator Action()
    {
        teleport.enabled = true;
        teleport.ShowTeleportHint();
        UpdateText("Now try to teleport to a zone.");
        yield return StartCoroutine(WaitForAction(tpAction));
        UpdateText("Good job!");
    }
}