using Unity.Netcode;
using UnityEngine;

public class AssignPlayerID : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            if (BattleManager.Instance != null)
            {
                BattleManager.Instance.localPlayerId = IsServer ? 0 : 1;
                Debug.Log(BattleManager.Instance.localPlayerId);
            }
        }
    }
}
