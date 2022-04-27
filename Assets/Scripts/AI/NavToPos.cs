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
    public AudioSource footstep;
    public AudioSource hurt;
    public AudioSource[] win;

    private float attRateCounter;
    private float distanceSave;
    private GameObject getPosFrom;
    private GameObject save;
    private float timeLeft = 0.05f;
    private int len;
    private bool isBurning = false;
    private float burningTime = 3f;
    private float GLORY;
    private float stepRate = 0.25f;


    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Tower");
        len = targets.Length;
        agent.speed = speed;
        attRateCounter = attRate;
        GLORY = Random.Range(0f, 10f);
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
        getPosFrom = null;
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
            if (el != null)
            {
                float distance = Vector3.Distance(transform.position, el.transform.position);
                if (distance < distanceSave)
                {
                    distanceSave = distance;
                    save = el;
                }
            }
        }
        getPosFrom = save;

        if (burningTime<=0)
        {
            burningTime = 3f;
            gameObject.GetComponent<StatsPotato>().health -= 1;
            hurt.Play();
        }

        if (distanceSave > attRange || getPosFrom==null)
        {
            gameObject.GetComponent<StateManager>().StopAttack();
            attRateCounter = attRate;
            if (getPosFrom != null)
            {
                stepRate -= Time.deltaTime;
                if (stepRate<=0)
                {
                    footstep.Play();
                    stepRate = 0.25f;
                }
                agent.SetDestination(getPosFrom.transform.position);
            }
            else
            {
                agent.SetDestination(transform.position);
                GLORY -= Time.deltaTime;
                if (GLORY <= 0)
                {
                    GLORY = Random.Range(5f,15f);
                    win[Random.Range(0, 2)].Play();
                }
            }
        }
        else
        {
            gameObject.GetComponent<StateManager>().StartAttack();
            agent.SetDestination(transform.position);
            attRateCounter -= Time.deltaTime;
            if (attRateCounter<=0)
            {
                getPosFrom.GetComponent<StatsTower>().health -= gameObject.GetComponent<StatsPotato>().attackPower;
                attRateCounter = attRate;
            }
        }
    }
}
