using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DeathMenu; 

    public GameObject damageOverlay;

    [SerializeField]
    private float maxHealth = 100.0f;

    [SerializeField]
    private float health = 100.0f;

    [SerializeField]
    private float iFrames = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Stage 1")
        {
            Debug.Log("stage 1");
            PlayerPrefs.SetFloat("maxHealth", 100);
        }

        maxHealth = PlayerPrefs.GetFloat("maxHealth");
        health = maxHealth;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public bool takeDamage(float damage)
    {
        bool hit = false;

        if(iFrames <= 0)
        {
            health -= damage;

            iFrames = 0.5f;

            if(health <= 0)
            {
                kill();
            }

            hit = true;

            if(damageOverlay != null)
            {
                DamgeOverlayScript dOverlay = damageOverlay.GetComponent<DamgeOverlayScript>();

                dOverlay.onDamage();
            }

        }

        return hit;
    }

    public void Heal(float healAmount)
    {
        health += healAmount;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void kill()
    {
        DeathMenuScript menu = DeathMenu.GetComponent<DeathMenuScript>();

        menu.onDeath();

        Time.timeScale = 0;
    }

    public float getHealth()
    {
        return health / 100;
    }

    public void IFrames(float frames)
    {
        iFrames = frames;
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
