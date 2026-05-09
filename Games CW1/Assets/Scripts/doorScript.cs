using UnityEngine;
using UnityEngine.UIElements;

public class doorScript : MonoBehaviour
{
    private float initialRotation;
    public bool opening = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = transform.rotation.eulerAngles.y;
    }

    public void open()
    {
        opening = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(opening && transform.rotation.eulerAngles.y <= initialRotation + 90)
        {
            Debug.Log(transform.rotation.y);
            Debug.Log(initialRotation);
            

            transform.Rotate(0, 0.5f, 0);
        }
    }
}
