using UnityEngine;
using UnityEngine.AI;

public class GolemScript : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;

    private Physics hit;

    public float cooldownTime = 1.5f;
    public float cooldown = 0;

    public float punchCooldownTime = 1.5f;
    public float punchCooldown = 0;
    
    public bool punch = false;
    public float punchTime = 1;

    [SerializeField]
    private GameObject projectile;

    public float delayTime = 1.41f;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("w");

        if((player.transform.position - transform.position).sqrMagnitude < 100)
        {
            transform.rotation.SetLookRotation(player.transform.position);
        }

        if((player.transform.position - transform.position).sqrMagnitude < 13)
        {
            if(punchCooldown <= 0)
            {
                punch = true;
                animator.SetBool("Punch", true);

                punchCooldown = punchCooldownTime;

                StartCoroutine(stopPunch(punchTime));
            }
        }
        else if((player.transform.position - transform.position).sqrMagnitude < 100)
        {
            target(player.transform.position);
            punch = false;
        }
        else
        {
            punch = false;
        }

        cooldown -= 1 * Time.deltaTime;
        punchCooldown -= 1* Time.deltaTime;
    }

    public void fistCollide()
    {
        if (punch)
        {
            player.GetComponent<PlayerHealth>().takeDamage(30);
        }
    }

    System.Collections.IEnumerator stopPunch(float time)
    {
        yield return new WaitForSeconds(time);

        punch = false;
        animator.SetBool("Punch", false);
    }

    private void target(Vector3 target)
    {        
        Debug.Log("target");

        if(cooldown <= 0)
        {
            animator.SetBool("Throw", true);

            StartCoroutine(attackSpawn(delayTime, target));

            cooldown = cooldownTime;
        } 
    }

    System.Collections.IEnumerator attackSpawn(float time, Vector3 target)
    {
        Debug.Log("Wait");

        yield return new WaitForSeconds(time);

        animator.SetBool("Throw", false);

        Debug.Log("Wait2");

        Debug.Log(transform.position);

        GameObject rock = Instantiate(projectile, transform.position + transform.rotation * new Vector3(0, 2, 2), new Quaternion(0,0,0,0));

        rock.GetComponent<Rigidbody>().AddForce((target - rock.transform.position) * 10000);
    }
}
