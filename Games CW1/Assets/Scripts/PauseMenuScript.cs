using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    private PlayerActions actions;

    public GameObject menu;

    public Button Resume;
    public Button Settings;
    public Button QuitButton;


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
    }

    private void openMenu()
    {
        toggleMenu();
    }

    private void exitGame()
    {
        Debug.Log("gameEnd");
        Application.Quit();
    }

    private void toggleMenu()
    {
        bool enableMenu = false;

        if(menu.GetComponent<Image>().enabled == false)
        {
            enableMenu = true;
        }

        menu.GetComponent<Image>().enabled = enableMenu;

        for(int i = 0; i < menu.transform.childCount; i++)
        {
            menu.transform.GetChild(i).GetComponent<Button>().enabled = enableMenu;
            menu.transform.GetChild(i).GetComponent<Text>().enabled = enableMenu;
        }
        
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
        
    }
}
