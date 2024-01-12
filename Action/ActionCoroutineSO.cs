using System.Collections;
using UnityEngine;

public class ActionCoroutineSO : ScriptableObject
{
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    
    public Coroutine StartActionCoroutine(MonoBehaviour mono, System.Action action)
    {
        return mono.StartCoroutine(ActionCoroutine(action));
    }

    public void StopActionCoroutine(MonoBehaviour mono, Coroutine coroutine)
    {
        mono.StopCoroutine(coroutine);
    }

    private IEnumerator ActionCoroutine(System.Action action)
    {
        while (true)
        {
            action.Invoke();
            yield return _waitForFixedUpdate;
        }
    }
    /*Example of how this could be called.
    private ActionCoroutineSO _actionCoroutine;
    private Coroutine _currentCoroutine;
    
     _currentCoroutine = _actionCoroutine.StartActionCoroutine(this, () =>
        {
            Debug.Log(_mousePosition);
        });
    
    _actionCoroutine = ScriptableObject.CreateInstance<ActionCoroutineSO>();
    */
}