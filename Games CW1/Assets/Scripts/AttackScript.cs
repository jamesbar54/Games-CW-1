using System;
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




    //attack delay timer
    private float attackDelay = 0;
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
        if(Time.time > attackDelay && attackBool)
        {
            //Debug.Log("active");

            Debug.Log("Player Collide: " + collision.gameObject);

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
    }

    public void activateAttack()
    {
        
        attackDelay = Time.time + attackDelayValue;

        attackBool = true;

        attacking = true;
    }

    public void endAttack()
    {
        attacking = false;
        attackBool = false;
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
