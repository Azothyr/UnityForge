using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class NavAgentBehaviorBase : MonoBehaviour
{ // Mouse click to move NavMeshAgent
    private WaitForFixedUpdate _wffuObj;
    [Header("Nav Mesh Components")]
    public NavMeshAgent ai;
    [SerializeField] private Vector3 destination;

    private void Awake()
    {
        _wffuObj = new WaitForFixedUpdate();
        ai = GetComponent<NavMeshAgent>();
    }

    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        ai.SetDestination(destination);
    }

    public abstract void Move(Vector3 dest);
}

