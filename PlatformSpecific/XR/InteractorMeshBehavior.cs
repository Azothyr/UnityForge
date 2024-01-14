using UnityEngine;

public class InteractorMeshBehavior: MonoBehaviour
{
    [SerializeField] private GameObject model;
    
    public void Show()
    {
        model.SetActive(false);
    }
    
    public void Hide()
    {
        model.SetActive(false);
    }
}
