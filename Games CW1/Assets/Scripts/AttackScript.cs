using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackScript : MonoBehaviour
{

    [SerializeField]
    private bool attacking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
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
