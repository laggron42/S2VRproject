using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ArrowCollider : MonoBehaviour
{
    public GameObject popPrefab;
    public SoundPlayOneshot collisionSound;

    void ApplyDamage()
    {
        GameObject particleObject = Instantiate(popPrefab, transform.position, transform.rotation) as GameObject;
        particleObject.GetComponent<ParticleSystem>().Play();
        Destroy(particleObject, 2f);
        gameObject.GetComponent<StatsPotato>().health -= 1;
    }
}
