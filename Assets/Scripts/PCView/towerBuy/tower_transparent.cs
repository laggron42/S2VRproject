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
            TilesManager.instance.GetXY(hit.point, out int x, out int z);
            Vector3 worldPos = new Vector3(x, 0, z);
            transform.position = worldPos;
        }

        // when you click then place the tower
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(tower, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
