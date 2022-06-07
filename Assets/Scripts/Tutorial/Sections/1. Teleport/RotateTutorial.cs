using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RotateTutorial : TutorialStep
{
    public SteamVR_Action_Boolean snapLeftAction = SteamVR_Input.GetBooleanAction("SnapTurnLeft");
    public SteamVR_Action_Boolean snapRightAction = SteamVR_Input.GetBooleanAction("SnapTurnRight");

    public override IEnumerator Action()
    {
        UpdateText("Try to rotate with the joystick.");
        foreach (Hand hand in player.hands)
        {
            ControllerButtonHints.HideAllTextHints(hand);
            ControllerButtonHints.ShowButtonHint(hand, snapLeftAction, snapRightAction);
            if (hand != null)
            {
                ControllerButtonHints hints = ControllerButtonHints.GetControllerButtonHints( hand );
                if (hints != null && hints.autoSetWithControllerRangeOfMotion)
                    hand.SetTemporarySkeletonRangeOfMotion(SkeletalMotionRangeChange.WithController);
            }
        }
        yield return StartCoroutine(WaitForAction(snapLeftAction, snapRightAction));
        foreach (Hand hand in player.hands)
        {
            ControllerButtonHints.HideAllButtonHints(hand);
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