using Unity.Services.Multiplayer;
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

    public void attack()
    {
        if(turnManager.enemyPlayer.isShielded == true)
        {
            turnManager.curPlayer.Hp -= 5;
        }
        else
            turnManager.enemyPlayer.Hp -= 10;


        endTurn();
    }

    public void defend()
    {
        turnManager.curPlayer.isShielded = true;

        endTurn();
    }

    public void counter()
    {
        turnManager.enemyPlayer.isShielded = false;
        turnManager.enemyPlayer.Hp -= 5;

        endTurn();
    }

    public void skip()
    {
        turnManager.curPlayer.isShielded = false;

        endTurn();
    }

    void endTurn()
    {
        turnManager.enemyPlayer.isShielded = false;
        turnManager.UpdateTurn();
    }


}
