using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavToPos : MonoBehaviour
{
    private GameObject[] targets;
    public NavMeshAgent agent;
    public float speed;
    public float attRate = 1f;
    public float attRange = 3f;

    private float attRateCounter;
    private float distanceSave;
    private GameObject getPosFrom;
    private GameObject save;
    private float timeLeft = 0.05f;
    private int len;
    private bool isBurning = false;
    private float burningTime = 3f;
    

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Tower");
        len = targets.Length;
        agent.speed = speed;
        attRateCounter = attRate;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        GameObject projectile = collision.collider.gameObject;
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        float rbSpeed = rb.velocity.sqrMagnitude;
        if ( projectile.CompareTag("projectile") && rbSpeed > 0.1f)
        {
            gameObject.GetComponent<StatsPotato>().health -= 1;
        }
    }*/

    void Update()
    {
        distanceSave = float.MaxValue;
        timeLeft -= Time.deltaTime;
        if (gameObject.GetComponent<Valve.VR.InteractionSystem.FireSource>().isBurning)
        {
            burningTime -= Time.deltaTime;
        }
        if (timeLeft<=0)
        {
            targets = GameObject.FindGameObjectsWithTag("Tower");
            len = targets.Length;
            timeLeft = 0.05f;
        }
        foreach (var el in targets)
        {
            float distance = Vector3.Distance(transform.position, el.transform.position);
            if (distance < distanceSave)
            {
                distanceSave = distance;
                save = el;
            }
        }
        getPosFrom = save;

        if (burningTime<=0)
        {
            burningTime = 3f;
            gameObject.GetComponent<StatsPotato>().health -= 1;
        }

        if (distanceSave > attRange || len==0)
        {
            gameObject.GetComponent<StateManager>().StopAttack();
            attRateCounter = attRate;
            if (len != 0)
            {
                agent.SetDestination(getPosFrom.transform.position);
            }
        }
        else
        {
            gameObject.GetComponent<StateManager>().StartAttack();
            attRateCounter -= Time.deltaTime;
            if (attRateCounter<=0)
            {
                getPosFrom.GetComponent<StatsTower>().health -= gameObject.GetComponent<StatsPotato>().attackPower;
                attRateCounter = attRate;
            }
        }
    }
}
