using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuScript : MonoBehaviour
{
    public GameObject menu;

    public GameObject EndScreen;

    public Button Retry;
    public Button GiveUp;
    public Button ERR;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Retry.onClick.AddListener(resetScene);
        
        GiveUp.onClick.AddListener(giveUp);
    }

    public void onDeath()
    {
        EndScreen.GetComponent<Image>().enabled = true;
        
        Retry.GetComponent<Text>().enabled = true;
        GiveUp.GetComponent<Text>().enabled = true;
        Retry.GetComponent<Button>().enabled = true;
        GiveUp.GetComponent<Button>().enabled = true;
    }

    private void resetButtons()
    {
        ERR.Select();
    }

    private void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        resetButtons();

        Time.timeScale = 1;
    }

    private void giveUp()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
