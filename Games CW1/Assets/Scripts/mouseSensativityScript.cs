using UnityEngine;
using UnityEngine.UI;

public class mouseSensativityScript : MonoBehaviour
{ 
    public Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();

        slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});

        slider.value = PlayerPrefs.GetFloat("mouseSen");
    }

    void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat("mouseSen", slider.value);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().upadateMouse();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
