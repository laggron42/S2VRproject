using System.Collections;
using TMPro;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Management;
using Valve.VR;

public class Loader : MonoBehaviour
{
    public GameObject buttons;
    public GameObject infoPanel;
    public TMP_Text status;
    public TMP_Text errorTitle;
    public Button leaveButton;

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
    
    private void DisplayError(string message)
    {
        if (errorTitle.IsActive())
            return; // Don't display multiple messages
        errorTitle.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);
        status.text = message;
    }

    private void Start()
    {
        if (!RuntimeReady())
            return;
        Debug.Log("OpenVR runtime available. Starting VR...");
        StartCoroutine(StartVR());
    }

    private bool RuntimeReady()
    {
        if (!OpenVR.IsRuntimeInstalled())
        {
            DisplayError("OpenVR was not found.");
            return false;
        }

        if (!OpenVR.IsHmdPresent())
        {
            DisplayError("No headset was detected.");
            return false;
        }

        return true;
    }

    private IEnumerator StartVR()
    {
        if (XRGeneralSettings.Instance == null)
        {
            DisplayError("XR SDK instance not available.");
            yield break;
        }

        if (XRGeneralSettings.Instance.Manager == null)
        {
            DisplayError("XR SDK manager not found.");
            yield break;
        }

        XRManagerSettings manager = XRGeneralSettings.Instance.Manager;

        if (manager.activeLoaders.Count < 1 || !manager.activeLoaders[0] is OpenVRLoader)
        {
            DisplayError("OpenVR loader was not found.");
            yield break;
        }

        yield return null;
        
        OpenVRLoader loader = manager.activeLoaders[0] as OpenVRLoader;

        bool success = loader.Initialize();
        if (!success)
        {
            DisplayError("Failed to initialize OpenVR loader. Is the headset connected?");
            yield break;
        }

        Debug.Log("Initialized OpenVR loader");
        yield return null;

        success = loader.Start();
        if (!success)
        {
            DisplayError("Failed to start OpenVR loader.");
            loader.Deinitialize();
            yield break;
        }
        
        Debug.Log("Started OpenVR loader");
        yield return null;
        
        SteamVR.Initialize();
        SteamVR_Events.Initialized.AddListener(SteamVRReady);
        if (SteamVR.initializedState == SteamVR.InitializedStates.InitializeSuccess)
        {
            infoPanel.SetActive(false);
            buttons.SetActive(true);
        }
    }
    
    private void SteamVRReady(bool initialized)
    {
        if (!initialized)
        {
            DisplayError("An unknown error occured.");
            return;
        }
        if (!SteamVR.usingNativeSupport)
        {
            DisplayError("No headset was detected.");
            return;
        }
        
        infoPanel.SetActive(false);
        buttons.SetActive(true);
    }
}