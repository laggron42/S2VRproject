using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

/// <summary>
/// A step of a tutorial
/// </summary>
public abstract class TutorialStep : MonoBehaviour
{
    public bool enabledStep = true;
    [HideInInspector]
    public TMP_Text messageText;
    protected Player player;
    
    private void Start()
    {
        player = Player.instance;
    }

    /// <summary>
    /// Update the content of the TMP text attached
    /// </summary>
    /// <param name="content">New content</param>
    protected void UpdateText(string content)
    {
        messageText.text = content;
    }
    
    /// <summary>
    /// Wait until action was pressed, then return.
    /// </summary>
    /// <param name="action">Corresponding SteamVR boolean action</param>
    protected IEnumerator WaitForAction(ISteamVR_Action_Boolean action)
    {
        while (!action.stateUp)
            yield return null;
    }
    
    /// <summary>
    /// The action that has to be done for this step of the tutorial.
    /// Return once complete.
    /// </summary>
    public abstract IEnumerator Action();
}