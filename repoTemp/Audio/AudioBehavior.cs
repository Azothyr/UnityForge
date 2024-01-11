using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioBehavior : MonoBehaviour
{
    public string volume = "VolumeParameter";
    public AudioMixer mixer;
    public Slider slider;
    public float multiplier = 30f;
    public Toggle toggle;
    private bool disabledToggleEvent;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volume, slider.value);
    }

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    private void HandleToggleValueChanged(bool enableSound)
    {

        if (disabledToggleEvent)
        {
            return;
        }
        
        if(enableSound)
        {
            slider.value = PlayerPrefs.GetFloat(volume, slider.value);
        }
        else
        {
            PlayerPrefs.SetFloat(volume, slider.value);
            slider.value = slider.minValue;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volume, slider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        mixer.SetFloat(volume, Mathf.Log10(value) * multiplier);
        disabledToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        disabledToggleEvent = false;
    }
    
}
