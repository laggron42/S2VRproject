using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPLOSION : MonoBehaviour
{
    // Start is called before the first frame update
    public static void DeadZone(Transform trans, ParticleSystem kaboom)
    {

        var L = Physics.OverlapSphere(trans.position, 10);
        ParticleSystem p = Instantiate(kaboom, trans.position, trans.rotation);
        p.transform.parent = null;
        p.Play();
        foreach (var el in L)
        {
            if (el.CompareTag("Potato"))
            {
                el.GetComponent<Valve.VR.InteractionSystem.FireSource>().StartBurning();
                el.GetComponent<StatsPotato>().health -= 3;
            }
            if (el.CompareTag("Green"))
            {
                el.GetComponent<Valve.VR.InteractionSystem.FireSource>().StartBurning();
            }
        }
        Destroy(p, 5f);
    }
    public static void DeadZone(Vector3 trans, ParticleSystem kaboom)
    {

        var L = Physics.OverlapSphere(trans, 10);
        ParticleSystem p = Instantiate(kaboom, trans, Quaternion.identity);
        p.transform.parent = null;
        p.Play();
        foreach (var el in L)
        {
            if (el.CompareTag("Potato"))
            {
                el.GetComponent<Valve.VR.InteractionSystem.FireSource>().StartBurning();
                el.GetComponent<StatsPotato>().health -= 3;
            }
            if (el.CompareTag("Green"))
            {
                el.GetComponent<Valve.VR.InteractionSystem.FireSource>().StartBurning();
            }
        }
        Destroy(p, 5f);
    }
}
