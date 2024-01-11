using UnityEngine;

public class DestroyBehavior : MonoBehaviour
{
    private float seconds;

    public void TriggerDestroy() => Destroy(gameObject);
    
    public void DestroySelfAndChild()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        
        Destroy(gameObject);
    }
}
