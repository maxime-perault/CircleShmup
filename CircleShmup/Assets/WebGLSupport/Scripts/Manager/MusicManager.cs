using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

/**
 * @class MusicManager
 */
public class MusicManager : MonoBehaviour
{
    public float SFX_Volume  = 50;
    public float Main_Volume = 50;

    public AudioMixerGroup SFXMixer;
    public AudioMixerGroup MainMixer;

    public GameObject       soundObjet;
    public List<SoundEvent> events  = new List<SoundEvent>();
    public List<GameObject> targets = new List<GameObject>();

    private static MusicManager instance;

    /**
     * Called when the object is loaded
     */
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    /**
     * Check for terminated sounds
     */
    void Update()
    {
        for(int nObject = targets.Count - 1; nObject >= 0; --nObject)
        {
            AudioSource source = targets[nObject].GetComponent<AudioSource>();
         
            if (!source.isPlaying)
            {
                Destroy(targets[nObject]);
                targets.RemoveAt(nObject);
            }
        }
    }

    /**
     * Plays a sound
     */
    public static void PostEvent(string name)
    {
        SoundEvent soundEvent = MusicManager.instance.GetClipFromName(name);

        if(soundEvent == null)
        {
            return;
        }

        // Process event type
        if(soundEvent.Play)
        {
            MusicManager.instance.PlayEvent(soundEvent);
        }
        else if(soundEvent.Stop)
        {
            MusicManager.instance.StopEvent(soundEvent);
        }
        else
        {
            Debug.Log("Event has no mode");
        }
    }

    /**
     * Temporary method before RTPC system
     */
    public static void SetPitchDistance(string name, float distance)
    {
        for (int nObject = MusicManager.instance.targets.Count - 1; nObject >= 0; --nObject)
        {
            AudioSource source = MusicManager.instance.targets[nObject].GetComponent<AudioSource>();

            if (MusicManager.instance.targets[nObject].name == name)
            {
                float pitch = 1.0f + 0.2f * distance;
                source.pitch = pitch;
            }
        }
    }

    /**
     * Start an event
     */
    private void PlayEvent(SoundEvent soundEvent)
    {
        if(GetTargetInstanceCount(soundEvent.SoundEventTarget.name) >= soundEvent.SoundEventMaxInstance)
        {
            return;
        }

        GameObject  go     = Instantiate(soundObjet, this.transform);
        AudioSource source = go.GetComponent<AudioSource>();

        // Setting up the name
        go.name = soundEvent.SoundEventTarget.name;

        // Chosing the right mixer
        if(soundEvent.SoundEventType == SoundEvent.Type.SFX)
        {
            source.outputAudioMixerGroup = SFXMixer;
        }
        else
        {
            source.outputAudioMixerGroup = MainMixer;
        }
            
        source.loop                  = soundEvent.SoundEventLoop;
        source.clip                  = soundEvent.SoundEventTarget;
        source.volume                = soundEvent.SoundEventVolume;
        source.pitch                 = soundEvent.SoundEventPitch;
        source.reverbZoneMix         = soundEvent.SoundEventReverb;

        if(soundEvent.RandomizePitch)
        {
            source.pitch += Random.Range(soundEvent.PitchRandomRange.x, soundEvent.PitchRandomRange.x);
        }
        
        if(soundEvent.RandomizeVolume)
        {
            source.volume += Random.Range(soundEvent.VolumeRandomRange.x, soundEvent.VolumeRandomRange.y);
        }

        source.Play();
        targets.Add(go);

        if(go.name == "Main_Menu_UI_Validate")
        {
            Debug.Log("Break");
            Debug.Break();
        }
    }

    /**
     * Removes all occurences of the target event
     */
    private void StopEvent(SoundEvent soundEvent)
    {
        for (int nObject = targets.Count - 1; nObject >= 0; --nObject)
        {
            AudioSource source = targets[nObject].GetComponent<AudioSource>();

            if (targets[nObject].name == soundEvent.SoundEventTarget.name)
            {
                source.Stop();
                Destroy(targets[nObject]);
                targets.RemoveAt(nObject);
            }
        }
    }

    /**
     * Gets back the sound event from the event name
     */
    private SoundEvent GetClipFromName(string name)
    {
        foreach(SoundEvent soundEvent in events)
        {
            if(soundEvent.SoundEventName == name)
            {
                return soundEvent;
            }
        }
        
        return null;
    }

    /**
     * Returns the number of playing target event
     */
    private int GetTargetInstanceCount(string name)
    {
        int count = 0;
        foreach (GameObject go in targets)
        {
            if (go.name == name)
            {
                count += 1;
            }
        }

        return count;
    }
}
