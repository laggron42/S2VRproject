using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Tooltip("Is this entire section active?")]
    public bool enabledSection = true;

    [Tooltip("Delay between each step of the tutorial.")]
    public float delay = 2.0f;
    
    public TMP_Text messageText;
    public List<TutorialStep> steps;

    public IEnumerator RunTutorial()
    {
        foreach (TutorialStep step in steps)
        {
            step.messageText = messageText;
            if (!step.enabledStep)
                continue;
            yield return step.Action();
            yield return new WaitForSeconds(delay);
        }
    }
}
