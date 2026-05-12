using UnityEngine;

public class rockScript : MonoBehaviour
{
    public bool onRebound = false;
    private GameObject golem;
    private Rigidbody rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        golem = GameObject.FindGameObjectWithTag("Golem");
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth pHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if(pHealth)
        {
            pHealth.takeDamage(25);
        }

        if(collision.gameObject.tag != "Player" && onRebound)
        {
            Debug.Log(collision.gameObject.tag);
        }
        

        HealthScript eHealth = collision.gameObject.GetComponent<HealthScript>();

        if(eHealth && onRebound)
        {
            eHealth.takeDamage(60);
        }

        if((collision.gameObject.tag != "Golem" && !onRebound) || (collision.gameObject.tag != "Player" && onRebound))
        {
            FindFirstObjectByType<AudioManager>().play("rockSmash");

            Destroy(gameObject);
        }
    }

    public void rebound()
    {
        Debug.Log("REBOUND");

        rigidBody.linearVelocity = new Vector3(0,0,0);

        rigidBody.AddForce((golem.transform.position + new Vector3(0,1.5f,0) - transform.position) * 6000);

        onRebound = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
