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
