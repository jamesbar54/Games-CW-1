using UnityEngine;

public class particleKill : MonoBehaviour
{
    public ParticleSystem particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();

        Destroy(gameObject, particle.main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
