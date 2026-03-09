using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyTarget : MonoBehaviour
{
    [Header("Objects")]
    public Vector3 targetLocation;

    public GameObject target;

    public NavMeshAgent agent;

    [Header("Variables")]
    public float dist = 3.0f;
    public float dist2 = 15.0f;
    public float speed = 10.0f;

    void Awake()
    {
        agent.speed = speed;
        agent.SetDestination(targetLocation);
    }


    //Update is called once per frame
    void Update()
    {
        if((target.transform.position - transform.position).sqrMagnitude > dist2)
        {
            agent.SetDestination(transform.position);
        }
        else if((target.transform.position - transform.position).sqrMagnitude > dist)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }
}
