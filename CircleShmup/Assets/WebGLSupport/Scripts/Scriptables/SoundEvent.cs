#if UNITY_EDITOR
    using UnityEditor;
#endif

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about a sound event
 * @class SoundEvent
 */
[CreateAssetMenu(fileName = "SoundEvent", menuName = "Shmup/SoundEvent")]
public class SoundEvent : ScriptableObject
{
    [SerializeField]
    public enum Type
    {
        SFX   = 0,
        Music = 1
    }

    [SerializeField] public bool      Play;
    [SerializeField] public bool      Stop;
    [SerializeField] public AudioClip SoundEventTarget;
    [SerializeField] public string    SoundEventName;

    [SerializeField] public Type      SoundEventType;
    [SerializeField] public bool      SoundEventLoop;
    [SerializeField] public int       SoundEventMaxInstance;
    [SerializeField] public float     SoundEventVolume;
    [SerializeField] public float     SoundEventPitch;
    [SerializeField] public float     SoundEventReverb;

    [SerializeField] public bool      RandomizeVolume;
    [SerializeField] public bool      RandomizePitch;
    [SerializeField] public Vector2   PitchRandomRange;
    [SerializeField] public Vector2   VolumeRandomRange;

    /**
     * Avoid object reset when the scene change or after play/stop
     */
    private void OnEnable()
    {
        SoundEventName = this.name;
    }

    /**
     * Avoid object reset when the scene change or after play/stop
     */
    private void OnDisable()
    {
        #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
        #endif
    }
}