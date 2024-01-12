using UnityEngine;
#if UNITY_NETCODE
using Unity.Netcode.Components;

public class ClientNetworkTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;
    }

    protected override void Update()
    {
        CanCommitToTransform = IsOwner;
        base.Update();
        if (NetworkManager == null) return;
        if (NetworkManager.IsConnectedClient || NetworkManager.IsListening)
        {
            TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);    
        }
    }
    
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
#else
public class ClientNetworkTransform : MonoBehaviour
{
    void Start()
    {
        Debug.LogError("The ClientNetworkTransform class is only used in a Netcode multiplayer environment and should not be included in builds without Netcode.");
    }
}
#endif