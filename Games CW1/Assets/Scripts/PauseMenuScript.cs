using Palmmedia.ReportGenerator.Core;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private PlayerActions actions;

    public GameObject menu;

    public Button Resume;
    public Button Settings;
    public Button QuitButton;
    public Button ERR;

    public Mask mask;
    private float maskMove = -1400;
    private bool settingsActive = false;
    public Button backButton;

    public GameObject deathScript;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        actions = new PlayerActions();
        actions.Menu.MenuKey.performed += cxt => openMenu();

        Resume.onClick.AddListener(toggleMenu);
        QuitButton.onClick.AddListener(exitGame);
        Settings.onClick.AddListener(SettingsActivate);
        backButton.onClick.AddListener(SettingsDeactivate);

        resetButtons();
    }

    private void resetButtons()
    {
        ERR.Select();
    }

    private void openMenu()
    {
        if (!settingsActive && !deathScript.GetComponent<DeathMenuScript>().menuActive())
        {
            toggleMenu();
        }    
    }

    private void SettingsActivate()
    {
        toggleMenu();
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        settingsActive = true;

        mask.rectTransform.Translate(Vector3.left * -1400);
    }

    private void SettingsDeactivate()
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        settingsActive = false;

        mask.rectTransform.Translate(Vector3.left * 1400);
    }

    private void exitGame()
    {
        Debug.Log("gameEnd");

        resetButtons();

        Application.Quit();
    }

    private void toggleMenu()
    {
        bool enableMenu;

        if(menu.GetComponent<Image>().enabled == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            enableMenu = true;

            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            enableMenu = false;

            Time.timeScale = 1;
        }     

        menu.GetComponent<Image>().enabled = enableMenu;

        for(int i = 0; i < menu.transform.childCount; i++)
        {
            menu.transform.GetChild(i).GetComponent<Button>().enabled = enableMenu;
            menu.transform.GetChild(i).GetComponent<Text>().enabled = enableMenu;
        }
        

        //resetButtons();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        resetButtons();        
    }
}
