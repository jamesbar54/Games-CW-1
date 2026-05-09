using UnityEngine;

public class doorTriggerScript : MonoBehaviour
{
    public GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        door.GetComponent<doorScript>().openingTrigger();
    }

    void OnTriggerExit(Collider other)
    {
        door.GetComponent<doorScript>().closingTrigger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
