using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class DamgeOverlayScript : MonoBehaviour
{
    public CanvasRenderer img;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img.SetAlpha(0.0f);
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
