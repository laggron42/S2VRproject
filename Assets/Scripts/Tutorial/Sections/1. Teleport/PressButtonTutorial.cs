using System.Collections;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PressButtonTutorial : TutorialStep
{
    public ISteamVR_Action_Boolean menuAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("OpenMenu");
    
    public override IEnumerator Action()
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
}