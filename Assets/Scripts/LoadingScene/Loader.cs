using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
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
}
