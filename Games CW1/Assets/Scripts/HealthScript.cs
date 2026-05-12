using System.Threading.Tasks;
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

    public float deathDelay = 10;
    private float deathTime = float.PositiveInfinity;

    private bool fade = false;

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

            Debug.Log("damage");

            EnemyTarget enemyTarget = gameObject.GetComponent<EnemyTarget>();

            if (enemyTarget)
            {
                enemyTarget.Stagger();
            }
        }
    }

    private void death()
    {
        EnemyTarget enemyTarget = gameObject.GetComponent<EnemyTarget>();

        if (enemyTarget)
        {
            enemyTarget.killed();
        }
        else
        {
            gameObject.GetComponent<GolemScript>().killed();
        }

        deathTime = Time.time + deathDelay;
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

        if (deathTime < Time.time)
        {
            SceneChangeScript changer = gameObject.GetComponentInParent<SceneChangeScript>();

            if(changer != null)
            {
                changer.checker(gameObject);
            }

            gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
        }

        if(gameObject.transform.localScale.y <= 0)
        {
            Debug.Log("kill now");

            Destroy(gameObject);
        }

    }
}
