using UnityEngine;
using UnityEngine.InputSystem;

public class playerPrefrences : MonoBehaviour
{
    public PlayerActions playerActions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerActions = new PlayerActions();
        playerActions.Controls.Attack.ApplyBindingOverride("<keycoard>k");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
