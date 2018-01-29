using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for sounds event
 * @class SoundEventInspector
 */
[CustomEditor(typeof(SoundEvent))]
public class SoundEventInspector : Editor
{
    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        // Buffering info
        SoundEvent soundEvent = (SoundEvent)target;
        InspectorHelper.DisplaySeparator("Informations");
        soundEvent.SoundEventName = EditorGUILayout.TextField("Name", soundEvent.SoundEventName);

        EditorGUILayout.Space();
        InspectorHelper.DisplaySeparator("Event type");
        soundEvent.Play = EditorGUILayout.Toggle("Type Play", soundEvent.Play);
        soundEvent.Stop = EditorGUILayout.Toggle("Type Stop", soundEvent.Stop);

        EditorGUILayout.Space();
        InspectorHelper.DisplaySeparator("Settings");
        if(soundEvent.Play)
        {
            soundEvent.SoundEventType        = (SoundEvent.Type)EditorGUILayout.EnumPopup(soundEvent.SoundEventType);
            soundEvent.SoundEventLoop        = EditorGUILayout.Toggle     ("Loop ", soundEvent.SoundEventLoop);
            soundEvent.SoundEventTarget      = EditorGUILayout.ObjectField(soundEvent.SoundEventTarget, typeof(AudioClip), false) as AudioClip;
            soundEvent.SoundEventMaxInstance = EditorGUILayout.IntField   ("Max instance", soundEvent.SoundEventMaxInstance);
            soundEvent.SoundEventVolume      = EditorGUILayout.FloatField ("Volume", soundEvent.SoundEventVolume);
            soundEvent.SoundEventPitch       = EditorGUILayout.FloatField ("Pitch ", soundEvent.SoundEventPitch);
            soundEvent.SoundEventReverb      = EditorGUILayout.FloatField ("Reverb", soundEvent.SoundEventReverb);

            soundEvent.RandomizePitch  = EditorGUILayout.Toggle("Randomize pitch ", soundEvent.RandomizePitch);
            soundEvent.RandomizeVolume = EditorGUILayout.Toggle("Randomize volume", soundEvent.RandomizeVolume);

            if(soundEvent.RandomizePitch)
            {
                soundEvent.VolumeRandomRange = EditorGUILayout.Vector2Field("Volume random range", soundEvent.VolumeRandomRange);
            }

            if(soundEvent.RandomizeVolume)
            {
                soundEvent.PitchRandomRange = EditorGUILayout.Vector2Field("Pitch random range  ", soundEvent.PitchRandomRange);
            }
        }

        if(soundEvent.Stop)
        {
            soundEvent.SoundEventTarget = EditorGUILayout.ObjectField(soundEvent.SoundEventTarget, typeof(AudioClip), false) as AudioClip;
        }

        // Editor bug fix with custom window
        EditorUtility.SetDirty(soundEvent);
    }
}