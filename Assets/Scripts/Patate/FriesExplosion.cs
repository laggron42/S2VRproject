using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriesExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    public static void BOOM(GameObject fries, Transform trans)
    {
        trans.parent = null;
        for (int i =0; i<6;i++)
        {
            GameObject g = Instantiate(fries,new Vector3(trans.position.x,trans.position.y,trans.position.z),Quaternion.Euler(new Vector3(Random.Range(0f,360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
            Vector3 trNext = new Vector3(Random.Range(-200, 200), Random.Range(500,700), Random.Range(-200, 200));
            g.GetComponent<Rigidbody>().AddForce(trNext);
            Destroy(g, 5.0f);
        }
    }
}
