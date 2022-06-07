using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float delayBeforeStart = 3.0f;
    public List<Tutorial> tutorials;

    private void Start()
    {
        Invoke("StartTutorial", delayBeforeStart);
    }

    private void StartTutorial()
    {
        StartCoroutine(RunAllTutorials());
    }

    IEnumerator RunAllTutorials()
    {
        foreach (Tutorial tutorial in tutorials)
            yield return tutorial.RunTutorial();
    }
}
