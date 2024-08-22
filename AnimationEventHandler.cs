using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    public UnityEvent animationEvent;
    // Example of an event function that takes no parameters
    public void AnimationEvent()
    {
        // Do something when the animation event is triggered
        animationEvent.Invoke();
        // Debug.Log("Animation event triggered!");
    }

   /*// Example of an event function that takes an AnimationEvent parameter
    public void MyEventFunctionWithParameter(AnimationEvent animationEvent)
    {
        // Access properties of the AnimationEvent
        Debug.Log("Animation event time: " + animationEvent.time);
        Debug.Log("Animation event string parameter: " + animationEvent.stringParameter);
    }*/
}