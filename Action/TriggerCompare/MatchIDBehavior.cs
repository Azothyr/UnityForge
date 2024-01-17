using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MatchIDBehavior : IDBehavior
{
   [Serializable]
   public struct PossibleMatch
   {
      public ID nameIdObj;
      public UnityEvent triggerEvent;
   }

   private WaitForFixedUpdate wffuObj = new WaitForFixedUpdate();
   public List<PossibleMatch> triggerEnterMatches;
   private ID otherIdObj;

   private void Awake()
   {
      foreach (var obj in triggerEnterMatches)
      {
         var possibleMatch = obj;
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<IDBehavior>() == null) return;
      otherIdObj = other.GetComponent<IDBehavior>().idObj;
      StartCoroutine(CheckId(otherIdObj, triggerEnterMatches));
   }
   
   private IEnumerator CheckId(ID nameId, List<PossibleMatch> possibleMatches)
   {
      otherIdObj = nameId;
      foreach (var obj in possibleMatches.Where(obj => otherIdObj == obj.nameIdObj))
      {
         obj.triggerEvent.Invoke();
         
         yield return wffuObj;
      }
   }
}
