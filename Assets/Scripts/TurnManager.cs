using Unity.Netcode;
using UnityEngine;

public class TurnManager : NetworkSingleton<TurnManager>
{
    
    public NetworkVariable<int> currentTurn = new NetworkVariable<int>(0);
    public int turn = 0;

    [ServerRpc(RequireOwnership = false)]
    public void EndTurnServerRpc()
    {
        //if(!IsHost) return;
        currentTurn.Value = 1 - currentTurn.Value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
