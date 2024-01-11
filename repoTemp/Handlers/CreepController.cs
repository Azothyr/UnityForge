using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavAgentBehavior))]
public class CreepController : MonoBehaviour, IDamagable, IDamageDealer
{
    public CreepData creepData;
    
    private NavAgentBehavior _agentBehavior;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = creepData.speed;
        _agent.radius = creepData.radius;
        _agent.height = creepData.height;
    }
    
    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void DealDamage(IDamagable target, float amount)
    {
        throw new System.NotImplementedException();
    }
}
