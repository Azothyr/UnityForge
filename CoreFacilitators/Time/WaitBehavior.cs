using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaitBehavior : MonoBehaviour
{
    [SerializeField] private string purpose;
    public UnityEvent endWaitForSeconds, endWaitForZero;
    
    private int waitAmount;
    private WaitForSeconds wfsObj = new WaitForSeconds(1);
    private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();

    public void startWaitForSecondsEvent(int seconds)
    {
        StartCoroutine(WaitForSecondsEvent(seconds));
    }

    public void startWaitForZeroIntDataEvent(IntData data)
    {
        StartCoroutine(WaitForZeroIntDataEvent(data));
    }
    
    private IEnumerator WaitForZeroIntDataEvent(IntData obj)
    {
        waitAmount = obj.value;
        
        while (waitAmount > 0)
        {
            waitAmount = obj.value;
            yield return wffuObj;
        }
        endWaitForZero.Invoke();
    }
    
    private IEnumerator WaitForSecondsEvent(int num)
    {
        waitAmount = num;
        
        while (waitAmount > 0)
        {
            waitAmount--;
            yield return wfsObj;
        }
        endWaitForSeconds.Invoke();
    }
}
