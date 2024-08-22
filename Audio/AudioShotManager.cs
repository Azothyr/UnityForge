using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioShotManager", menuName = "Audio/Audio Shot Manager", order = 0)]
public class AudioShotManager : ScriptableObject
{
    [System.Serializable]
    public class AudioShot
    {
        public string id;
        public AudioClip clip;
        public bool played, playOnAwake;
        [Range(0, 256)] public int priority = 128;
        [Range(0, 1)] public float volume = 1.0f;
        [Range(-3, 3)] public float pitch = 1.0f;
        public float delay;
        public UnityEvent onComplete;
    }

    public List<AudioShot> audioShots;
    private Coroutine _waitForEndCoroutine;

    public void PlayAudio(AudioSource audioSource, string id)
    {
        var index = audioShots.FindIndex(shot => shot.id == id);
        PlayAudio(audioSource, index);
    }

    public void PlayAudio(AudioSource audioSource, int index)
    {
        if (index < 0 || index >= audioShots.Count) return;
        var shot = audioShots[index];
        if (shot.played || shot.clip == null || audioSource == null) return;
        audioSource.PlayOneShot(shot.clip);
        shot.played = true;
    }

    public void ResetAllAudioShots()
    {
        foreach (var shot in audioShots)
        {
            shot.played = false;
        }
    }

    public void ResetAudioShot(string id) { ProcessAudioShot(id, false); }
    public void ResetAudioShot(int index) { ProcessAudioShot(index, false); }
    public void LockAudioShot(string id) { ProcessAudioShot(id, true); }
    public void LockAudioShot(int index) { ProcessAudioShot(index, true); }
    
    private void ProcessAudioShot(string id, bool setTo)
    {
        var index = audioShots.FindIndex(shot => shot.id == id);
        ProcessAudioShot(index, setTo);
    }

    private void ProcessAudioShot(int index, bool setTo)
    {
        if (index < 0 || index >= audioShots.Count) return;

        audioShots[index].played = setTo;
    }
}