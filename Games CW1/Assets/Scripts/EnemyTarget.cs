using UnityEngine;
using UnityEngine.AI;

public class EnemyTarget : MonoBehaviour
{
    [Header("Objects")]
    public Vector3 targetLocation;

    public GameObject target;

    public NavMeshAgent agent;

    public Animator animator;

    [Header("Variables")]
    public float dist = 3.0f;
    public float dist2 = 15.0f;
    public float speed = 10.0f;

    private bool attacking = false;
    private float damageTimer = 0;
    public float setTime = 0.88f;

    private bool dead = false;

    void Awake()
    {
        agent.speed = speed;
        agent.SetDestination(targetLocation);
    }


    //Update is called once per frame
    void Update()
    {

        if (!dead)
        {
            Vector3 tar = target.transform.position;

            if((tar - transform.position).sqrMagnitude > dist2)
            {
                agent.SetDestination(transform.position);
                idle();
                
                attacking = false;
            }
            else if((tar - transform.position).sqrMagnitude > dist)
            {
                agent.SetDestination(tar);
                running();

                attacking = false;
            }
            else
            {
                if (!attacking)
                {
                    damageTimer = Time.time - 0.3f;
                    Debug.Log(damageTimer);
                }
                agent.SetDestination(transform.position);
                attack();
            }
        }
        else
        {
            death();
        }

    }

    private void death()
    {
        Debug.Log("dead");

        animator.SetBool("Dead", true);
        animator.SetBool("Attacking", false);

    
    }


    public void killed()
    {
        Debug.Log("killed");

        dead = true;
    }

    private void idle()
    {
        animator.SetBool("Running", false);
        animator.SetBool("Attacking", false);

    }

    private void running()
    {
        animator.SetBool("Running", true);
        animator.SetBool("Attacking", false);

    }

    private void attack()
    {
        animator.SetBool("Attacking", true);

        Debug.Log("Attack");
        attacking = true;

        if(damageTimer + setTime < Time.time)
        {    

            Debug.Log("Hit");

            PlayerHealth health = target.GetComponent<PlayerHealth>();

            health.takeDamage(5);

            damageTimer = Time.time;
            
        }
    }
}
