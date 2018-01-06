using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about a wave of enemies
 * @class Wave
 */
[System.Serializable]
public class Wave
{
    [SerializeField] public uint        WaveID;
    [SerializeField] public string      WaveName;
    [SerializeField] public bool        WaveBlocking;
    [SerializeField] public float       WaveTiming;
    [SerializeField] public SpawnerData WaveSpawner;

    /**
     * Default constructor
     */
    public Wave()
    {
        WaveID         = 0;
        WaveName       = "Unknown";
        WaveBlocking   = true;
        WaveTiming     = 0.0f;
        WaveSpawner    = null;
    }

    /**
     * Copy constructor
     */
    public Wave(Wave other)
    {
        WaveID         = other.WaveID;
        WaveName       = other.WaveName;
        WaveBlocking   = other.WaveBlocking;
        WaveTiming     = other.WaveTiming;
        WaveSpawner    = other.WaveSpawner;
    }

    /**
     * Constructs a Wave from parameters list 
     */
    public Wave(uint id, string name, bool blocking, float timing, SpawnerData spawner)
    {
        WaveID         = id;
        WaveName       = name;
        WaveBlocking   = blocking;
        WaveTiming     = timing;
        WaveSpawner    = spawner;
    }

    /**
     * Returns an estimation of the wave duration
     */
    public float GetWaveDuration()
    {
        if(WaveSpawner != null)
        {
            return WaveSpawner.GetLastTiming();
        }

        return 0.0f;
    }
}
