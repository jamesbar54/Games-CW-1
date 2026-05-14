using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GolemScript : MonoBehaviour
{
    
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject golemThrow;

    private Physics hit;

    public float cooldownTime = 1.5f;
    public float cooldown = 0;
    
    public bool punch = false;
    public float punchTime = 1;

    [SerializeField]
    private GameObject projectile;

    public float delayTime = 1.41f;

    public Animator animator;

    public Vector3 spawnPos = new Vector3(1, 2, 1.8f);

    private bool stop = false;
    private AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if((player.transform.position - transform.position).sqrMagnitude < 100)
            {
                transform.rotation.SetLookRotation(player.transform.position);

                transform.LookAt(player.transform);
            }

            if((player.transform.position - transform.position).sqrMagnitude < 13)
            {
                if(cooldown <= 0)
                {
                    punch = true;
                    animator.SetBool("Punch", true);

                    cooldown = cooldownTime;

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
        }
    }

    public void killed()
    {
        Debug.Log("killed");

        animator.SetBool("killed", true);

        StartCoroutine(kill());
    }

    System.Collections.IEnumerator kill()
    {
        Debug.Log("kill1");

        stop = true;

        yield return new WaitForSeconds(5);

        Debug.Log("kill2");

        SceneManager.LoadScene("EndGame");


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
            golemThrow.GetComponent<GolemThrowScript>().throwRock();

            animator.SetBool("Throw", true);

            StartCoroutine(attackSpawn(delayTime));

            cooldown = cooldownTime;
        } 
    }

    System.Collections.IEnumerator attackSpawn(float time)
    {
        Debug.Log("Wait");

        audioManager.play("rockRumble");

        yield return new WaitForSeconds(time);

        animator.SetBool("Throw", false);

        Debug.Log("Wait2");

        Debug.Log(transform.position);

        GameObject rock = Instantiate(projectile, transform.position + transform.rotation * spawnPos, new Quaternion(0,0,0,0));

        rock.GetComponent<Rigidbody>().AddForce((player.transform.position + new Vector3(0, 1, 0) - rock.transform.position) * 10000);
        rock.GetComponent<Rigidbody>().AddTorque(new Vector3(10000, -10000, 0));
    }

    public void damage()
    {
        animator.SetBool("Damage", true);

        StartCoroutine(damageEnd(0.5f));

        cooldown = cooldownTime * 0.7f;
    }

    System.Collections.IEnumerator damageEnd(float time)
    {
        yield return new WaitForSeconds(time);

        animator.SetBool("Damage", false);
    }
}
