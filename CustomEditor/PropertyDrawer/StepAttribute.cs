using UnityEngine;

public class StepAttribute : PropertyAttribute
{
    public float Step;

    public StepAttribute(float step)
    {
        this.Step = step;
    }
}