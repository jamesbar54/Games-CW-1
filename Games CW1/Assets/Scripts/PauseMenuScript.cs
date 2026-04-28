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

        resetButtons();
    }

    private void resetButtons()
    {
        ERR.Select();
    }

    private void openMenu()
    {
        if(Time.timeScale == 1){
            toggleMenu();
        }
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
            enableMenu = true;

            Time.timeScale = 0;
        }
        else
        {
            enableMenu = false;

            Time.timeScale = 1;
        }     

        menu.GetComponent<Image>().enabled = enableMenu;

        for(int i = 0; i < menu.transform.childCount; i++)
        {
            menu.transform.GetChild(i).GetComponent<Button>().enabled = enableMenu;
            menu.transform.GetChild(i).GetComponent<Text>().enabled = enableMenu;
        }
        

        resetButtons();
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
