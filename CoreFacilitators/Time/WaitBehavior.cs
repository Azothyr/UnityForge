using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitBehavior : MonoBehaviour
{

    [System.Serializable]
    public struct StringIdEvent
    {
        public string id;
        public UnityEvent onWaitFinished;
    }

    [System.Serializable]
    public struct SecondsAndStringIdEvent
    {
        public string id;
        public bool realTime;
        public float seconds;
        public UnityEvent onWaitFinished;
    }

    [System.Serializable]
    public struct IntDataAndStringIdEvent
    {
        public string id;
        public IntData data;
        public UnityEvent onWaitFinished;
    }
    
    
    public List<SecondsAndStringIdEvent> endWaitForSeconds;
    public List<IntDataAndStringIdEvent> endWaitForZero;
    public List<StringIdEvent> endWaitForFixedUpdate;

    private float _secondsToWait;
    private int _waitAmount;
    private IntData _intData;
    private readonly WaitForSeconds _wfms = new (0.1f);
    private readonly WaitForSeconds _wfs = new (1);
    private readonly WaitForSecondsRealtime _wfmrts = new (0.1f);
    private readonly WaitForSecondsRealtime _wfrts = new (1);
    private readonly WaitForFixedUpdate _wffu = new ();

    private bool CheckActive()
    {
        return gameObject.activeInHierarchy;
        
    }
    
    public void StartWaitForSecondsEvent(string eventID)
    {
        if (!CheckActive()) return;
        var seconds = endWaitForSeconds.Find(x => x.id == eventID).seconds;
        StartCoroutine(WaitForSecondsEvent(seconds, eventID));
    }

    public void StartWaitForSecondsEvent(float seconds, string eventID)
    {
        if (!CheckActive()) return;
        StartCoroutine(WaitForSecondsEvent(seconds, eventID));
    }

    public void StopWaitForSecondsEvent(string eventID)
    {
        Debug.Log("StopWaitForSecondsEvent");
        StopCoroutine(WaitForSecondsEvent(endWaitForSeconds.Find(x => x.id == eventID).seconds, eventID));
    }

    public void StartWaitForZeroIntDataEvent(string eventID)
    {
        if (!CheckActive()) return;
        var data = endWaitForZero.Find(x => x.id == eventID).data;
        StartCoroutine(WaitForZeroIntDataEvent(data, eventID));
    }

    public void StartWaitForZeroIntDataEvent(IntData data, string eventID)
    {
        if (!CheckActive()) return;
        StartCoroutine(WaitForZeroIntDataEvent(data, eventID));
    }

    public void StopWaitForZeroIntDataEvent(string eventID)
    {
        StopCoroutine(WaitForZeroIntDataEvent(endWaitForZero.Find(x => x.id == eventID).data, eventID));
    }

    public void StartWaitForFixedUpdateEvent(string eventID)
    {
        StartCoroutine(WaitForFixedUpdateEvent(eventID));
    }

    public void StopWaitForFixedUpdateEvent(string eventID)
    {
        StopCoroutine(WaitForFixedUpdateEvent(eventID));
    }

    private IEnumerator WaitForSecondsEvent(float seconds, string eventID)
    {
        var runInRealTime = endWaitForSeconds.Find(x => x.id == eventID).realTime;

        if (runInRealTime)
        {
            float elapsed = 0f;

            while (elapsed < seconds)
            {
                elapsed += Time.unscaledDeltaTime;
                yield return null; 
            }
            
            var eventToInvoke = endWaitForSeconds.Find(x => x.id == eventID);
            eventToInvoke.onWaitFinished?.Invoke();
        } else 
        {
            _secondsToWait = seconds;
            if (_secondsToWait <= 0) yield break;
            while (_secondsToWait > 0)
            {
                if (_secondsToWait > 0)
                {
                    _secondsToWait--;
                    yield return _wfs;
                }
                else
                {
                    _secondsToWait -= 0.1f;
                    yield return _wfms;
                }
            }
        }
        
        foreach (var item in endWaitForSeconds)
        {
            if (item.id != eventID) continue;
            item.onWaitFinished.Invoke();
            break;
        }
    }

    private IEnumerator WaitForZeroIntDataEvent(IntData obj, string eventID)
    {
        _waitAmount = obj.value;
        if (_waitAmount <= 0) yield break;
        while (_waitAmount > 0)
        {
            _waitAmount = obj.value;
            yield return _wffu;
        }

        foreach (var item in endWaitForZero)
        {
            if (item.id != eventID) continue;
            item.onWaitFinished.Invoke();
            break;
        }
    }

    private IEnumerator WaitForFixedUpdateEvent(string eventID)
    {
        yield return _wffu;
        foreach (var item in endWaitForFixedUpdate)
        {
            if (item.id != eventID) continue;
            item.onWaitFinished.Invoke();
            break;
        }
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}