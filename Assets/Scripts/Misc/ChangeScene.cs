using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartSceneChange(string scene)
    {
        LoadSceneData.nextSceneToLoad = scene;
        SceneManager.LoadSceneAsync("Loading");
    }
}
