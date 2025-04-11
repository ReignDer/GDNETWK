using Unity.Netcode;
using UnityEngine;

public class BattleSetUPs : NetworkBehaviour
{
    [SerializeField] private Transform playerSpawnPosition;
    [SerializeField] private Transform enemySpawnPosition;
    [SerializeField] private GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        var instance = Instantiate(playerPrefab);
        var networkObject = instance.GetComponent<NetworkObject>();
        instance.transform.position = playerSpawnPosition.position;
        networkObject.Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
