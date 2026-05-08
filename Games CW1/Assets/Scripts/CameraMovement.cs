using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform aimAt;
    [SerializeField]
    private Transform goTo;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void volumeUpdate()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicVolume") / 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aimAt);

        Rotate();
    }

    void Rotate()
    {
        transform.position += (goTo.position - transform.position) * 0.1f;
    }
}
