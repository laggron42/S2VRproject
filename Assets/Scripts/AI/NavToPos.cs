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
    public AudioSource[] win;

    private float attRateCounter;
    private float distanceSave;
    private GameObject getPosFrom;
    private GameObject save;
    private float timeLeft = 0.05f;
    private float burningTime = 2f;
    private float timeBeforeBurning = 0.5f;
    private float GLORY;
    private float stepRate = 0.25f;


    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Tower");
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

    /*void OnCollisionEnter(Collision collision)
    {
        win[Random.Range(0, 2)].Play(); 
    }*/

    void OnCollisionStay(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.GetComponent<Valve.VR.InteractionSystem.FireSource>() != null && go.GetComponent<Valve.VR.InteractionSystem.FireSource>().isBurning)
        {
            if (timeBeforeBurning <= 0 /*|| go.CompareTag("Green")*/)
                gameObject.GetComponent<Valve.VR.InteractionSystem.FireSource>().StartBurning();
            else timeBeforeBurning -= Time.deltaTime;
        }
    }
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

        if (burningTime<=0f)
        {
            burningTime = 2f;
            gameObject.GetComponent<StatsPotato>().health -= 1;
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
                getPosFrom.GetComponent<Stats>().health -= gameObject.GetComponent<StatsPotato>().attackPower;
                attRateCounter = attRate;
            }
        }
    }
}
