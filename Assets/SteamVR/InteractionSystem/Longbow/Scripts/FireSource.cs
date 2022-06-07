//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: This object can be set on fire
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
	//-------------------------------------------------------------------------
	public class FireSource : MonoBehaviour
	{
		public GameObject fireParticlePrefab;
		public bool startActive;
		public bool destroyAfter;
		private GameObject fireObject;

		public ParticleSystem customParticles;

		public bool isBurning;

		public float burnTime;
		public float ignitionDelay = 0;
		private float ignitionTime;

		private Hand hand;

		public AudioSource ignitionSound;

		public bool canSpreadFromThisSource = true;

		//-------------------------------------------------
		void Start()
		{
			if ( startActive )
			{
				StartBurning();
			}
		}


		//-------------------------------------------------
		void Update()
		{
			if ( ( burnTime != 0 ) && ( Time.time > ( ignitionTime + burnTime ) ) && isBurning )
			{
				isBurning = false;
				if ( customParticles != null )
				{
					customParticles.Stop();
				}
				else
				{
					Destroy( fireObject );
				}
				if ( destroyAfter )
					Destroy( gameObject );
			}
		}


		//-------------------------------------------------
		void OnTriggerEnter( Collider other )
		{
			if ( isBurning && canSpreadFromThisSource )
			{
				other.SendMessageUpwards( "FireExposure", fireParticlePrefab , SendMessageOptions.DontRequireReceiver );
			}
		}


		//-------------------------------------------------
		public void FireExposure(GameObject otherFireParticlePrefab = null)
		{
			if ( fireObject == null || fireObject != otherFireParticlePrefab )
			{
				StartCoroutine(StartBurningInTime(otherFireParticlePrefab));
			}

			if ( hand = GetComponentInParent<Hand>() )
			{
				hand.TriggerHapticPulse( 1000 );
			}
		}

		private IEnumerator StartBurningInTime(GameObject fireParticlePrefab = null)
		{
			yield return new WaitForSeconds(ignitionDelay);
			StartBurning(fireParticlePrefab);
		}


		//-------------------------------------------------
		public void StartBurning(GameObject otherFireParticlePrefab = null)
		{
			GameObject particlePrefab;
			if (otherFireParticlePrefab != null)
			{
				particlePrefab = otherFireParticlePrefab;
			}
			else
			{
				particlePrefab = this.fireParticlePrefab;
			}
			isBurning = true;
			ignitionTime = Time.time;

			// Play the fire ignition sound if there is one
			if ( ignitionSound != null )
			{
				ignitionSound.Play();
			}

			if ( customParticles != null )
			{
				customParticles.Play();
			}
			else
			{
				if ( particlePrefab != null )
				{
					fireObject = Instantiate( particlePrefab, transform.position, transform.rotation ) as GameObject;
					fireObject.transform.localScale = transform.localScale;
					fireObject.transform.parent = transform;
				}
			}
		}
	}
}
