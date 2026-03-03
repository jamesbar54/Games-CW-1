using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{

    [SerializeField]
    private bool attacking = false;

    [SerializeField]
    private float damage = 10.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collide");

        if(attacking == true)
        {
            GameObject gameObject = collision.gameObject;

            HealthScript healthScript = gameObject.GetComponent<HealthScript>();

            if (healthScript)
            {
                healthScript.takeDamage(damage);
            }
        }
    }

    public void activateAttack()
    {
        attacking = true;
    }

    public void endAttack()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
