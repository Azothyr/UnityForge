using UnityEngine;

[CreateAssetMenu (fileName = "FloatData", menuName = "Data/SingleValueData/FloatData")]
public class FloatData : ScriptableObject
{
    public bool zeroOnEnable;
    public float value;
    // private float _step;
    //
    // public float step { get; set; }
    // {
    //     _step = value;
    // }
    
    private void OnEnable()
    {
        value = (zeroOnEnable) ? 0 : value;
    }
    
    public void SetValue(float num)
    {
        value = num;
    }
    
    public void UpdateValue(float num)
    {
        value += num;
    }
    
    public void IncrementValue()
    {
        value++;
    }
    
    public void DecrementValue()
    {
        value--;
    }

    private float GetPlayPrefVal(string key)
    {
        value = (PlayerPrefs.HasKey(key)) ? PlayerPrefs.GetFloat(key) : 0;
        return value;
    }
    
    public void AddValueToPlayPref(string key)
    {
        value += GetPlayPrefVal(key);
        PlayerPrefs.SetFloat(key, value);
        value = 0;
        PlayerPrefs.Save();
    }
    
    public void SetValueToPlayPref(string key)
    {
        value = GetPlayPrefVal(key);
        PlayerPrefs.SetFloat(key, value);
    }
    
    public void SetPlayPrefToValue(string key)
    {
        PlayerPrefs.SetFloat(key, value);
    }
}