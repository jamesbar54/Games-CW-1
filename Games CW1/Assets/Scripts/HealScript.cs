using UnityEngine;

public class HealScript : MonoBehaviour
{
    [SerializeField]
    private float healAmount = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide");

        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

        if(health != null)
        {
            health.Heal(healAmount);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, 0.75f);
    }
}
