using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
    public Button health;
    public Button damage;

    public Mask mask;

    private bool active = false;

    public string nextScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        health.onClick.AddListener(increaseHealth);
        damage.onClick.AddListener(increaseDamage);
    }

    public void endLevel()
    {
        if (!active)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            health.enabled = true;
            damage.enabled = true;

            Time.timeScale = 0;

            mask.rectTransform.Translate(Vector3.right * 1300);

            active = true;
        }
    }

    private void increaseHealth()
    {
        //Debug.Log(PlayerPrefs.GetFloat("maxFloat"));

        PlayerPrefs.SetFloat("maxHealth", PlayerPrefs.GetFloat("maxHealth") + 20);

        switchScene();
    }

    private void increaseDamage()
    {
        PlayerPrefs.SetFloat("damage", PlayerPrefs.GetFloat("damage") + 10);

        switchScene();
    }

    private void switchScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
