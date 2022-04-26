using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TowerSelector : MonoBehaviour
{
    private Player player;
    [Tooltip("The list of registered towers. Should automatically be filled on start based on Tower Parent Object.")]
    public SwitchTowerTeleportType[] towers;
    
    [Tooltip("The action for triggering tower switching.")]
    public SteamVR_Action_Boolean selectorAction;
    private ISteamVR_Action_Boolean tpAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    
    [Tooltip("Platform where you land when selecting towers.")]
    public TeleportMarkerBase platformTp;

    [Tooltip("The object that contains all towers which will be collected.")]
    public GameObject towerParentObject;
    private bool isSelecting = false;

    void OnEnable()
    {
        player = Player.instance;
        selectorAction.AddOnStateDownListener(OnButtonPress, SteamVR_Input_Sources.Any);

        towers = towerParentObject.GetComponentsInChildren<SwitchTowerTeleportType>();
    }

    // Update is called once per frame
    void OnButtonPress(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource)
    {
        if (!isSelecting)
            StartCoroutine(Selector());
    }

    IEnumerator Selector()
    {
        isSelecting = true;
        Teleport.instance.arcDistance = 40;
        player.transform.position = platformTp.gameObject.transform.position;
        UpdateTowerTeleportPoints();
        while (true)
        {
            while (!tpAction.stateUp)
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
            if (player.transform.position.y < 20)
                break;
        }
        RevertTowerTeleportPoints();
        isSelecting = false;
        Teleport.instance.arcDistance = 5;
        yield return null;
    }

    void UpdateTowerTeleportPoints()
    {
        foreach (SwitchTowerTeleportType tp in towers)
        {
            tp.SetToPointMode();
        }
    }

    void RevertTowerTeleportPoints()
    {
        foreach (SwitchTowerTeleportType tp in towers)
        {
            tp.SetToAreaMode();
        }
    }
}
