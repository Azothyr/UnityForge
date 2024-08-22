using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentBehavior : MonoBehaviour
{ 
    public UnityEvent onCreepReachedDestination;
    
    private WaitForFixedUpdate _wffu;
    private NavMeshAgent _ai;
    public Transform destination;

    private void Awake()
    {
        _wffu = new WaitForFixedUpdate();
        _ai = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        StartEndPathCheck();
    }
    
    public void SetSpeed(float speed)
    {
        _ai.speed = speed;
    }
    
    public void SetRadius(float radius){
        _ai.radius = radius;
    }
    
    public void SetHeight(float height)
    {
        _ai.height = height;
    }

    public void Setup(Transform dest)
    {
        destination = dest;
        if (!_ai) _ai = GetComponent<NavMeshAgent>();
        if (_ai) _ai.SetDestination(destination.position);
    }

    private void StartEndPathCheck()
    {
        StartCoroutine(EndCheck());
    }

    private IEnumerator EndCheck()
    {
        while (true)
        {
            if (_ai.remainingDistance < 0.5f && _ai.hasPath)
            {
                onCreepReachedDestination.Invoke();
                yield break;
            }

            yield return _wffu;
        }
    }
    
    public void StopMovement()
    {
        _ai.isStopped = true;
    }
}
