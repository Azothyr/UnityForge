using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProBehavior : MonoBehaviour
{
    private TextMeshProUGUI textObj;
    public UnityEvent startEvent;

    private void Start()
    {
        textObj = GetComponent<TextMeshProUGUI>();
        startEvent.Invoke();
    }

    public void UpdateLabel(string text)
    {
        textObj.text = text.ToString(CultureInfo.InvariantCulture);
    }
    
    public void UpdateLabel(FloatData obj)
    {
        textObj.text = obj.value.ToString(CultureInfo.InvariantCulture);
    }

    public void UpdateLabel(IntData obj)
    {
        textObj.text = obj.value.ToString(CultureInfo.InvariantCulture);
    }    
    
    public void UpdateLabel(DoubleData obj)
    {
        textObj.text = obj.value.ToString(CultureInfo.InvariantCulture);
    }
    
    private string FormatTime(float num)
    {
        float hour = Mathf.FloorToInt(num / 3600);
        float minutes = Mathf.FloorToInt(num / 60) % 60;
        float seconds = Mathf.FloorToInt(num % 60);
        float milliseconds = Mathf.FloorToInt((num * 100 ) % 100);
        if (num > 3600)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hour, minutes, seconds);
        }
        if (num < 60)
        {
            return string.Format("{0:00} sec", seconds);
        }
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public void UpdateTextToTimeFormat(FloatData obj)
    {
        textObj.text = FormatTime(obj.value);
    }
}