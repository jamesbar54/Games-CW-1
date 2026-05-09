using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        door.GetComponent<doorScript>().open();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
