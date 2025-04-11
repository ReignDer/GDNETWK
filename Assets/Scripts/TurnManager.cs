using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Unity.Services.Multiplayer;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int turn = 1;
    List<PlayerInfo> playerList = new List<PlayerInfo>();
    public PlayerInfo curPlayer;
    public PlayerInfo enemyPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInfo player1 = new PlayerInfo();
        PlayerInfo player2 = new PlayerInfo();

        playerList.Add(player1);
        playerList.Add(player2);

        turn = 1;
        UpdateTurn();
    }

    public void UpdateTurn()
    {
        checkEndGame();
        

        if (turn == 0)
        {
            turn = 1;
            curPlayer = playerList[1];
            enemyPlayer = playerList[0];
        }
        else
        {
            turn = 0;
            curPlayer = playerList[0];
            enemyPlayer = playerList[1];
        }
    }

    void checkEndGame()
    {
        int Hp0 = playerList[0].Hp;
        int Hp1 = playerList[1].Hp;

        Debug.Log("Player " + turn);
        Debug.Log("Hp0 " + Hp0);
        Debug.Log("Hp1 " + Hp1);

        if(Hp0 == 0)
        {
            //end game
        }

        if (Hp1 == 0)
        {
            //end game
        }
    }
}
