using UnityEngine;

public class rockScript : MonoBehaviour
{
    private bool onRebound = false;
    private Rigidbody rigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth pHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if(pHealth)
        {
            pHealth.takeDamage(25);
        }

        HealthScript eHealth = collision.gameObject.GetComponent<HealthScript>();

        if(eHealth && onRebound)
        {
            eHealth.takeDamage(60);
        }

        if(collision.gameObject.tag != "Golem" && !onRebound || collision.gameObject.tag != "Player" && onRebound)
        {
            Destroy(gameObject);
        }
    }

    public void rebound()
    {
        Debug.Log("REBOUND");

        rigidBody.AddForce(rigidBody.linearVelocity * -1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
