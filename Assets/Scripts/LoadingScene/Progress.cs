using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        if (LoadSceneData.nextSceneToLoad != null)
            StartCoroutine(LoadScene(LoadSceneData.nextSceneToLoad));
        else
            Debug.LogWarning("No scene to load");
        }

    IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(2);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            slider.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(2);
    }
}
