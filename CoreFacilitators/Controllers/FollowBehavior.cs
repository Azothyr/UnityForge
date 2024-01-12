using System.Collections;
using UnityEngine;

public class FollowBehavior : MonoBehaviour
{
    public GameObject player;
    public bool yConstraint;
    private Vector3 initialPosition, playerTransform, transformConstrain;
    
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    private void Awake()
    {
        initialPosition = transform.position;
        StartCoroutine(FollowPlayer(initialPosition));
    }

    private IEnumerator FollowPlayer(Vector3 offsetDistance)
    {
        while(player)
        {
            playerTransform = player.transform.position;
            if (yConstraint)
            {
                transformConstrain = new Vector3(playerTransform.x, offsetDistance.y, playerTransform.z);
                transform.position = transformConstrain;
            }
            else
            {
                transform.position = offsetDistance + player.transform.position;
            }
            yield return wffuObj;
        }
    }
}
