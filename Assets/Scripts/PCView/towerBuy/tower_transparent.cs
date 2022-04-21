using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_transparent : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    private Camera cam;
    public GameObject tower;


    // Start is called before the first frame update
    void Start()
    {
        cam  = GameObject.FindGameObjectWithTag("PcCam").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // move with the mouse
        if (Physics.Raycast(ray, out hit, 50.0f, ~5))
        {
            TilesManager.instance.GetXZ(hit.point, out int x, out int z);
            if (TilesManager.instance.CanPlaceTower(x, z))
                transform.position = TilesManager.instance.GetWorldPosition(x, hit.point.y ,z) + Vector3.up * 1.1f;
        }
 
        // when you click then place the tower
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            TilesManager.instance.SetValue(transform.position, 1);
            TilesManager.instance.EnterEditMode();
            Destroy(gameObject);
        }
    }
}
