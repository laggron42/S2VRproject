using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera cam;
    private float speed = 20.0f;
    private float zoomSpeed = 20.0f;
    private float minZ = -40.0f;
    private float maxZ = 40.0f;
    private float minX = -40.0f;
    private float maxX = 40.0f;
    private float minY = 10.0f;
    private float maxY = 50.0f;
    public OpenShop openShop;


    private TilesManager grid;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Screen.width: " + Screen.width);
    }


    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (openShop.isOpen)
            return;
        
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            z -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            x += speed * Time.deltaTime;
        }

        // move z and x when cliqued
        if (Input.GetMouseButton(0))
        {
            x -= Input.GetAxis("Mouse X") * speed * Time.deltaTime * 10;
            z -= Input.GetAxis("Mouse Y") * speed * Time.deltaTime * 10;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cam.orthographicSize = cam.orthographicSize < 11 ? 10 :cam.orthographicSize - zoomSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cam.orthographicSize = cam.orthographicSize > 35 ? 36 :cam.orthographicSize + zoomSpeed * Time.deltaTime;
        }

        x = Mathf.Clamp(x, minX, maxX);
        y = Mathf.Clamp(y, minY, maxY);
        z = Mathf.Clamp(z, minZ, maxZ);

        transform.position = new Vector3(x, y, z);

    }

    public void LookAt(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(target.x + 10f, transform.position.y, target.z), 1f);
    }
}
