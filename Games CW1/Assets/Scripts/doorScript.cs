using UnityEngine;
using UnityEngine.UIElements;

public class doorScript : MonoBehaviour
{
    private float initialRotation;
    private bool opening1 = false;
    private bool opening2 = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
    }

    public void open()
    {
        opening1 = true;
    }

    public void openingTrigger()
    {
        opening2 = true;        
    }

    public void closingTrigger()
    {
        if (!opening1)
        {
            opening2 = false;  
        }      
    }

    // Update is called once per frame
    void Update()
    {
        if(opening1 && opening2)
        {
            Debug.Log(transform.rotation.y);
            Debug.Log(initialRotation);
            

            transform.Rotate(0, -30 * Time.deltaTime, 0);

            if(transform.rotation.eulerAngles.y <= initialRotation - 85 && transform.rotation.eulerAngles.y >= initialRotation - 95)
            {
                opening1 = false;
            }
        }
    }
}
