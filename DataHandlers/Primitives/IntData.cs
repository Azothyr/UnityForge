using UnityEngine;

[CreateAssetMenu (fileName = "IntData", menuName = "Data/SingleValueData/IntData")]
public class IntData : ScriptableObject
{
    public bool zeroOnEnable;
    public int value;
    
    private void OnEnable()
    {
        value = (zeroOnEnable) ? 0 : value;
    }

    public void SetValue(int num)
    {
        value = num;
    }

    public void SetValue(IntData obj)
    {
        value = obj.value;
    }

    public void CompareValue(IntData obj)
    {
        value = (value >= obj.value) ? value: obj.value;
    }
    
    public void IncrementValue()
    {
        value++;
    }
    
    public void DecrementValue()
    {
        value--;
    }
    
    public void UpdateValue(int num)
    {
        value += num;
    }
    
    public void UpdateValue(IntData obj)
    {
        value += obj.value;
    }

    private int GetPlayPrefVal(string key)
    {
        value = (PlayerPrefs.HasKey(key)) ? PlayerPrefs.GetInt(key) : 0;
        return value;
    }
    
    public void AddValueToPlayPref(string key)
    {
        value += GetPlayPrefVal(key);
        PlayerPrefs.SetInt(key, value);
        value = 0;
        PlayerPrefs.Save();
    }
    
    public void SetValueToPlayPref(string key)
    {
        value = GetPlayPrefVal(key);
        PlayerPrefs.SetInt(key, value);
    }
}
