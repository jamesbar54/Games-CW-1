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

    public string damageNoise = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void takeDamage(float damage, string type = null)
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

            if (damageNoise != "")
            {
                FindAnyObjectByType<AudioManager>().play(damageNoise);
            }

            if (enemyTarget)
            {
                enemyTarget.Stagger();
            }
            else
            {
                GolemScript golem = gameObject.GetComponent<GolemScript>();

                if (golem && type == "rock")
                {
                    golem.damage();
                }
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

            if(changer)
            {
                changer.checker(gameObject);
            }

            gameObject.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
        }

        if(gameObject.transform.localScale.y <= 0)
        {
            Debug.Log("kill now");

            Destroy(gameObject);
        }

    }
}
