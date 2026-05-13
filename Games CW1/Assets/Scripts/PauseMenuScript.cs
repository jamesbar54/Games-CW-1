using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private PlayerActions actions;

    public GameObject pauseMenu;

    public GameObject settingsMenu;

    public Button Resume;
    public Button Settings;
    public Button QuitButton;
    public Button ERR;

    // public Mask mask;
    // private float maskMove = 3550;
    public bool settingsActive = false;
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

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().volumeUpdate();

        //FindFirstObjectByType<AudioManager>().updateVolume();
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

        settingsMenu.SetActive(true);
    }

    private void SettingsDeactivate()
    {
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        settingsActive = false;

        settingsMenu.SetActive(false);

        toggleMenu();
    }

    private void exitGame()
    {
        Debug.Log("gameEnd");

        resetButtons();

        Application.Quit();
    }

    private void toggleMenu()
    {
        Debug.Log("toggle");

        if(!pauseMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0;

            pauseMenu.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = 1;

            pauseMenu.SetActive(false);
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
