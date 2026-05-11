using UnityEngine;

public class RockParticleScript : MonoBehaviour
{
    public ParticleSystem fallingRocks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(fallingRocks, transform.position, transform.rotation);
    }
}
