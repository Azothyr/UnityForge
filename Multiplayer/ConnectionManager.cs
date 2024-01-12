using UnityEngine;
# if UNITY_NETCODE
using Unity.Netcode;

public class ConnectionManager : MonoBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
#else
public class ConnectionManager : MonoBehaviour
{
    void Start()
    {
        Debug.LogError("The ConnectionManager class is only used in a Netcode multiplayer environment and should not be included in builds without Netcode.");
    }
}
#endif
