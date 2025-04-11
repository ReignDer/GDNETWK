using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : NetworkSingleton<BattleManager>
{
    public PokemonData playerPokemon;
    public PokemonData enemyPokemon;

    public Slider playerHPBar;
    public Slider enemyHPBar;
    public TextMeshProUGUI battleLog;

    public Button moveButton1;
    public Button moveButton2;

    public int localPlayerId;
    private bool playerMoveSelected = false;
    private bool enemyMoveSelected = false;
    private int playerMoveDamage;
    private int enemyMoveDamage;


    private void Start()
    {
        moveButton1.onClick.AddListener(() => UseMove(10));
        moveButton2.onClick.AddListener(() => UseMove(20));
    }

    private void Update()
    {
        bool isMyTurn = !playerMoveSelected || !enemyMoveSelected;
        moveButton1.interactable = isMyTurn;
        moveButton2.interactable = isMyTurn;
    }

    void UseMove(int damage)
    {
        UseMoveServerRpc(damage);
    }

    [ServerRpc(RequireOwnership = false)]
    void UseMoveServerRpc(int damage, ServerRpcParams param = default)
    {
        ulong clientId = param.Receive.SenderClientId;

        if (clientId == 0)
        {
            playerMoveDamage = damage;
            playerMoveSelected = true;
        }
        else
        {
            enemyMoveDamage = damage;
            enemyMoveSelected = true;
        }

        if (playerMoveSelected && enemyMoveSelected)
        {
            ResolveMovesServerRpc();
        }

        
    }

    private IEnumerator DelayedResolve()
    {
        yield return new WaitForSeconds(2f);
        
        enemyPokemon.TakeDamage(playerMoveDamage); 
        playerPokemon.TakeDamage(enemyMoveDamage); 

       
        playerMoveSelected = false;
        enemyMoveSelected = false;

        
        UpdateHealthBarsClientRpc();

        
        CheckWinCondition();
    }

    [ServerRpc]
    void ResolveMovesServerRpc()
    {
        ShowMoveSequenceClientRpc("Player 1 used Tackle!", "Player 2 used Ember!");
        StartCoroutine(DelayedResolve());
    }
    
    [ClientRpc]
    void UpdateHealthBarsClientRpc()
    {
        playerHPBar.value = playerPokemon.currentHP.Value;
        enemyHPBar.value = enemyPokemon.currentHP.Value;
    }
    void CheckWinCondition()
    {
        if (playerPokemon.currentHP.Value <= 0)
            EndGameClientRpc("Player 2 Wins!");
        else if (enemyPokemon.currentHP.Value <= 0)
            EndGameClientRpc("Player 1 Wins!");
    }

    [ClientRpc]
    void EndGameClientRpc(string result)
    {
        battleLog.text = result;
        moveButton1.interactable = false;
        moveButton2.interactable = false;
    }
    [ClientRpc]
    void ShowMoveSequenceClientRpc(string move1Text, string move2Text)
    {
        StartCoroutine(ShowMovesInSequence(move1Text, move2Text));
    }

    private IEnumerator ShowMovesInSequence(string move1, string move2)
    {
        UpdateBattleLogClientRpc(move1);      
        yield return new WaitForSeconds(1f); 
        UpdateBattleLogClientRpc(move2);   
        yield return new WaitForSeconds(1f); 
    }
    [ClientRpc]
    void UpdateBattleLogClientRpc(string msg)
    {
        battleLog.text = msg;
    }
}

