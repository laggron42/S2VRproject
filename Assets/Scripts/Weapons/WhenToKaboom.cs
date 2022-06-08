using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class WhenToKaboom : MonoBehaviour
{
    public ParticleSystem kaboom;
    public bool b = false;
    private Interactable interactable;

    private void Start()
    {
        interactable = this.GetComponent<Interactable>();
    }
    private void OnColisionEnter(Collider other)
    {
        if (interactable != null && interactable.attachedToHand != null) //don't explode in hand
            return;
        GameObject go = other.gameObject;
        if (go.CompareTag("Green")
            || go.CompareTag("Potato")
            || go.CompareTag("ContactExplosion"))
        {
            EXPLOSION.DeadZone(transform, kaboom);
            Destroy(gameObject);
        }
    }
}