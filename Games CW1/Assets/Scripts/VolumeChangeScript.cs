using UnityEngine;
using UnityEngine.UI;

public class VolumeChangeScript : MonoBehaviour
{
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});

        slider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat("musicVolume", slider.value);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().volumeUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
