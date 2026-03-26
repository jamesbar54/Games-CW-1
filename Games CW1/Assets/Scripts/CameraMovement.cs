using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform aimAt;
    [SerializeField]
    private Transform goTo;

    public float moveSpeed = 0.1f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aimAt);

        Rotate();
    }

    void Rotate()
    {
        transform.position = goTo.position;
    }
}
