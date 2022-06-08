using UnityEngine;
using System.Collections;
using Valve.VR.InteractionSystem;
using System.Collections.Generic;

public class WhenBoom : MonoBehaviour
{
    public ParticleSystem kaboom;
    public GameObject explodePartPrefab;
    public int explodeCount = 10;

    public float minMagnitudeToExplode = 1f;

    private Interactable interactable;

    private void Start()
    {
        interactable = this.GetComponent<Interactable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (interactable != null && interactable.attachedToHand != null) //don't explode in hand
            return;

        if (/*collision.impulse.magnitude > minMagnitudeToExplode*/collision.collider.CompareTag("Green")
            || collision.collider.CompareTag("Potato")
            || collision.collider.CompareTag("ContactExplosion"))
        {
            EXPLOSION.DeadZone(transform, kaboom);
            Destroy(this.gameObject);
        }
    }
}
