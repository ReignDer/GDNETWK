using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkScenemanager : Singleton<NetworkScenemanager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public async void changeScene()
    {
        
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("SCENE MANAGER LOADED");
            NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("SCENE MANAGER LOADED");
            NetworkManager.Singleton.SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
            
        }
    }
    
}
