using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_transparent : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject tower;


    // Start is called before the first frame update
    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f))
        {
            transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
        {
            transform.position = hit.point;
        }

        if (Input.GetMouseButton(0))
        {
            Instantiate(tower, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
