using UnityEngine;
using UnityEngine.UI;

public class ExitScript : MonoBehaviour
{
    public Button exit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        exit.onClick.AddListener(exitGame);
    }

    void exitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
