using UnityEngine;

public class ActionScripts : MonoBehaviour
{
    int player = 0;
    TurnManager turnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnManager = FindAnyObjectByType<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void attack()
    {
        if (player == turnManager.turn)
        {
            
        }
    }


}
