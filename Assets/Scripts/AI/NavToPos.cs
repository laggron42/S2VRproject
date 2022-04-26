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
    

    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Tower");
        agent.speed = speed;
        attRateCounter = attRate;
    }
    void Update()
    {
        distanceSave = float.MaxValue;
        timeLeft -= Time.deltaTime;
        if (timeLeft<=0)
        {
            targets = GameObject.FindGameObjectsWithTag("Tower");
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
        if (distanceSave > attRange)
        {
            gameObject.GetComponent<StateManager>().StopAttack();
            attRateCounter = attRate;
            if (getPosFrom != null)
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
