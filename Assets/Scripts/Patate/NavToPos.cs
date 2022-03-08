using UnityEngine;
using UnityEngine.AI;

public class NavToPos : MonoBehaviour
{
    public Vector3 positionToGo;
    public NavMeshAgent agent;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            agent.SetDestination(positionToGo);
        }
    }
}
