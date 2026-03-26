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

    private float damageTimer = 0;
    public float setTime = 5;

    void Awake()
    {
        agent.speed = speed;
        agent.SetDestination(targetLocation);
    }


    //Update is called once per frame
    void Update()
    {
        Vector3 tar = target.transform.position;

        if((tar - transform.position).sqrMagnitude > dist2)
        {
            agent.SetDestination(transform.position);
        }
        else if((tar - transform.position).sqrMagnitude > dist)
        {
            agent.SetDestination(tar);
        }
        else
        {
            agent.SetDestination(transform.position);
            attack();
        }
    }

    private void attack()
    {
        Debug.Log("Attack");

        if(damageTimer + setTime < Time.time)
        {    
            Debug.Log("Hit");

            PlayerHealth health = target.GetComponent<PlayerHealth>();

            health.takeDamage(5);

            damageTimer = Time.time;
        }
    }
}
