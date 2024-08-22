using System.Collections;
using UnityEngine;

public class CoroutineController : MonoBehaviour
{
    private static CoroutineController _instance = null;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Coroutine Run(IEnumerator routine)
    {
        return _instance?.StartCoroutine(routine);
    }
}