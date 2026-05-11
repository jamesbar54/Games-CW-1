using UnityEngine;

public class GolemThrowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject noPhysicsRock;

    public ParticleSystem slam;

    private float spawnDelay = 0.35f;

    private float destroyDelay = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //slam = gameObject.GetComponent<ParticleSystem>();
        slam.Pause();
    }

    public void throwRock()
    {
        StartCoroutine(RockWithDelay(spawnDelay));
    }

    System.Collections.IEnumerator RockWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        slam.Play();

        GameObject rock = Instantiate(noPhysicsRock, transform.position + new Vector3(0, -0.75f, 1), transform.rotation, transform);

        rock.transform.localScale = new Vector3(120,120,120);

        yield return new WaitForSeconds(destroyDelay);

        slam.Stop();

        Destroy(rock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
