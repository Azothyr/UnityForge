using JetBrains.Annotations;
using UnityEngine;

public class IDBehavior : MonoBehaviour
{
    [CanBeNull] public ID idObj;

    public void ChangeId(ID id)
    {
        idObj = id;
    }
}