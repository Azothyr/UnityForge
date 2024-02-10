using UnityEngine;

[CreateAssetMenu(fileName = "Debugger", menuName = "Debug/Debugger")]
public class Debugger : ScriptableObject
{
    public bool individualDebug = true;
    [SerializeField] private bool debugManagerExpected = true;
    private DebugManager _debugManager;

    private void OnEnable()
    {
        _debugManager = FindObjectOfType<DebugManager>();
        if (_debugManager == null && debugManagerExpected)
        {
            Debug.LogWarning("DebugManager not found in the scene.");
        }
    }

    private void Log(object message)
    {
        if (_debugManager.globalDebug  && individualDebug)
        {
            Debug.Log(message);
        }
    }
       
    public void OnDebug(string obj)
    {
        Debug.Log(obj);
    }
       
    public void OnDebug(float obj)
    {
        Debug.Log(obj);
    }
       
    public void OnDebug(bool obj)
    {
        Debug.Log(obj);
    }
       
    public void OnDebug(object obj)
    {
        Debug.Log(obj);
    }
       
    public void OnDebug(int obj)
    {
        Debug.Log(obj);
    }
}