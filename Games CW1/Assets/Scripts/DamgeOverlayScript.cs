using UnityEngine;
using UnityEngine.UI;

public class DamgeOverlayScript : MonoBehaviour
{
    public CanvasRenderer img;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img.SetAlpha(0.0f);
    }

    void Awake()
    {
        img.GetComponentInParent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        img.SetAlpha(img.GetAlpha() - 0.01f);
    }

    public void onDamage()
    {
        img.SetAlpha(0.5f);
    }
}
