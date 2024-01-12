using UnityEngine;

public class PlayerAgentBehavior : NavAgentBehaviorBase
{
    public override void Move(Vector3 destination)
    {
        ai.SetDestination(destination);
    }
}
