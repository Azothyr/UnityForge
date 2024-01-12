using UnityEngine;

[CreateAssetMenu(fileName = "Debugger", menuName = "Debug/Debugger")]
public class Debugger : ScriptableObject
{
       public bool individualDebug = true;
       private DebugManager debugManager;

       private void OnEnable()
       {
              debugManager = FindObjectOfType<DebugManager>();
              if (debugManager == null)
              {
                     Debug.LogWarning("DebugManager not found in the scene.");
              }
       }

       private void Log(object message)
       {
              if (debugManager.globalDebug  && individualDebug)
              {
                     Debug.Log(message);
              }
       }
       
       public void OnDebug(string obj)
       {
              Log(obj);
       }
       
       public void OnDebug(float obj)
       {
              Log(obj);
       }
       
       public void OnDebug(bool obj)
       {
              Log(obj);
       }
       
       public void OnDebug(object obj)
       {
              Log(obj);
       }
       
       public void OnDebug(int obj)
       {
              Log(obj);
       }
}