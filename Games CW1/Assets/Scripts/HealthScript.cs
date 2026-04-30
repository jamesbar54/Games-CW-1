using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private float iFrames = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void takeDamage(float damage)
    {
        if(iFrames <= 0)
        {
            health -= damage;

            iFrames = 0.5f;

            if(health <= 0)
            {
                death();
            }
        }
    }

    private void death()
    {
        gameObject.GetComponent<EnemyTarget>().killed();
    }

    public float getHealth()
    {
        return health / 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(iFrames > 0)
        {
            iFrames -= 1 * Time.deltaTime;
        }
    }
}
