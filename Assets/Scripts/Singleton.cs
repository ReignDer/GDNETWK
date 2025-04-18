using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        
    }
}

public abstract class StaticNetworkInstance<T> : NetworkBehaviour where T : NetworkBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnDestroy()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

public abstract class NetworkSingleton<T> : StaticNetworkInstance<T> where T : NetworkBehaviour
{
    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        
    }
}
