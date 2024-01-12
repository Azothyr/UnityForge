using UnityEngine;
#if UNITY_NETCODE
using Unity.Netcode;

[RequireComponent(typeof(PlayerAgentBehavior))]
public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    private PlayerAgentBehavior _agentBehavior;
    
    private CameraUtility _cameraUtility;
    private Camera _camera;
    private ClickData _clickData;
    private Vector2 _mousePosition;
    

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) {return;}
        _clickData = ScriptableObject.CreateInstance<ClickData>();
        _cameraUtility = ScriptableObject.CreateInstance<CameraUtility>();
        _camera = Camera.main;
        _agentBehavior = GetComponent<PlayerAgentBehavior>();
        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) {return;}
        
        inputReader.MoveEvent -= HandleMove;
    }

    private void HandleMove(Vector2 mousePosition)
    {
        var destination = _cameraUtility.GetHitPosition(_camera, mousePosition);
        _agentBehavior.Move(destination);
    }
}
#else

[RequireComponent(typeof(PlayerAgentBehavior))]
public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        Debug.LogError("The PlayerMovement class is only used in a Netcode multiplayer environment and should not be included in builds without Netcode.");
    }
}
#endif