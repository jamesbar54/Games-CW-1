using UnityEngine;

public class EnablerScript : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Animator>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
