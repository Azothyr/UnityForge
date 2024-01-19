using UnityEngine;

public class InteractorMeshBehavior: MonoBehaviour
{
    [SerializeField] private GameObject model;
    
    public void Show()
    {
        model.SetActive(true);
    }
    
    public void Hide()
    {
        model.SetActive(false);
    }
}
