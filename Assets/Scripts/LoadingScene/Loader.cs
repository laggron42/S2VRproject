using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

public class Loader : MonoBehaviour
{
    public GameObject buttons;
    public GameObject error;
    
    public static void LoadMenu()
    {
        LoadSceneData.nextSceneToLoad = "menu";
        SceneManager.LoadScene("Loading");
    }

    public static void LoadGame()
    {
        LoadSceneData.nextSceneToLoad = "Main map";
        SceneManager.LoadScene("Loading");
    }

    public static void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (OpenVR.IsHmdPresent())
            buttons.SetActive(true);
        else
            error.SetActive(true);
    }
}
