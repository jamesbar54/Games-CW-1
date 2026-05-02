using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject DeathMenu; 

    public GameObject damageOverlay;

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
                kill();
            }
        }

        if(damageOverlay != null)
        {
            DamgeOverlayScript dOverlay = damageOverlay.GetComponent<DamgeOverlayScript>();

            dOverlay.onDamage();
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
