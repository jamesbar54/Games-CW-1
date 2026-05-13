using UnityEngine;
using UnityEngine.UI;

public class sfxScrtipt : MonoBehaviour
{
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});

        slider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat("sfxVolume", slider.value);

        FindFirstObjectByType<AudioManager>().updateVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

