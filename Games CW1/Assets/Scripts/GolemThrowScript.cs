using UnityEngine;

public class GolemThrowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject noPhysicsRock;

    private float spawnDelay = 0.45f;

    private float destroyDelay = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void throwRock()
    {
        StartCoroutine(RockWithDelay(spawnDelay));
    }

    System.Collections.IEnumerator RockWithDelay(float time)
    {
        yield return new WaitForSeconds(time);

        GameObject rock = Instantiate(noPhysicsRock, transform.position + new Vector3(0, -1, 1), transform.rotation, transform);

        rock.transform.localScale = new Vector3(120,120,120);

        yield return new WaitForSeconds(destroyDelay);

        Destroy(rock);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
