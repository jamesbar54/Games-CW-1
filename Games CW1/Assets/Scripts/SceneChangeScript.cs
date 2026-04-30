using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void checker(GameObject obj)
    {
        Destroy(obj);

        Debug.Log("check");

        Debug.Log(transform.childCount);

        if(transform.childCount == 1)
        {
            Debug.Log("work");

            SceneManager.LoadScene("Stage 2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
