using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer SingletonRef;

    public float SFX_Volume = 50;
    public float Main_Volume = 50;

    void Awake()
    {
        if (SingletonRef == null)
        {
            SingletonRef = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
