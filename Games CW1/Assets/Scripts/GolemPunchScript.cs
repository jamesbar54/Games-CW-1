using UnityEngine;

public class GolemPunchScript : MonoBehaviour
{
    public GolemScript golemScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            golemScript.fistCollide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
