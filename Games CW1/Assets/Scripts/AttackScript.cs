using UnityEngine;

public class AttackScript : MonoBehaviour
{

    [SerializeField]
    private bool attacking = false;

    [SerializeField]
    private float damage = 10.0f;

    private bool attackBool = false;
    public float attackDelayValue  = 0.02f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("active");

        Debug.Log("Player Collide: " + collision.gameObject);

        //if(attacking == true)
        //{
            GameObject gameObject = collision.gameObject;

            HealthScript healthScript = gameObject.GetComponent<HealthScript>();

            if (healthScript)
            {
                healthScript.takeDamage(damage);
            }
        //}
    }


    public void activateAttack()
    {
        attackBool = true;

        attacking = true;
    }

    public void endAttack()
    {
        Debug.Log("end");

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(attackBool);
        // Debug.LogFormat("{0} : {1}",attackDelay, Time.time);
        // if(Time.time > attackDelay && attackBool)
        // {
        //     Debug.Log("active");
        // }
    }
}
