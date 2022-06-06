using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransparentWareHouse : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    private Camera cam;
    public TowerSelector towerSelector;


    // Start is called before the first frame update
    void Start()
    {
        cam  = GameObject.FindGameObjectWithTag("PcCam").GetComponent<Camera>();
        towerSelector = FindObjectOfType<TowerSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // move with the mouse
        if (Physics.Raycast(ray, out hit, 50.0f, ~5))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            TilesManager.instance.GetXZ(hit.point, out int x, out int z);
            if (TilesManager.instance.CanPlaceWareHouse(x, z))
                transform.position = TilesManager.instance.GetWorldPosition(x, hit.point.y , z) 
                    + new Vector3(1, 0, -1) * 0.5f + Vector3.up * 1.1f - new Vector3 (2, 0, -2);
        }

        // when you click then place the WareHouse
        if (Input.GetMouseButtonDown(0))
        {
            //faire la liste des WareHouse
            towerSelector.AddWareHouse(transform.position);
            TilesManager.instance.SetValueWareHouse(transform.position, 1);
            TilesManager.instance.ExitEditMode();
            Destroy(gameObject);
        }
    }
}
