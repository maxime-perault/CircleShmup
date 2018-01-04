using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about the current stage
 * @class Stage
 */
[System.Serializable]
public class Stage
{
    [SerializeField] public uint   StageID;
    [SerializeField] public string StageName;
    [SerializeField] public string StageDescription;
    [SerializeField] public float  StageTimeout;

    [SerializeField] public List<Wave> StageWaves = new List<Wave>();

    /**
     * Default constructor
     */
    public Stage()
    {
        StageID          = 0;
        StageName        = "Unknown";
        StageDescription = "Unknown";
        StageTimeout     = 0.0f;

        StageWaves.Clear();
    }

    /**
     * Copy constructor
     */
    public Stage(Stage other)
    {
        StageID          = other.StageID;
        StageName        = other.StageName;
        StageDescription = other.StageDescription;
        StageTimeout     = other.StageTimeout;
        StageWaves       = other.StageWaves;
    }

    /**
     * Constructs a stage from parameters list 
     */
    public Stage(uint id, string name, string description, float timeout, List<Wave> waves)
    {
        StageID          = id;
        StageName        = name;
        StageDescription = description;
        StageTimeout     = timeout;
        StageWaves       = waves;
    }

    /**
     * Returns the total duration of the stage
     * @return The duration of the stage
     */
    public float GetStageDuration()
    {
        Wave lastet = GetLatestWave();
        return lastet.WaveTiming + lastet.WaveDuration;
    }

    /**
     * Returns the latest waves
     * @return The latest chronological wave
     */
    public Wave GetLatestWave()
    {
        Wave latestWave = StageWaves[0];

        int waveCount = StageWaves.Count;
        for(int nWave = 0; nWave < waveCount; ++nWave)
        {
            if(StageWaves[nWave].WaveTiming > latestWave.WaveTiming)
            {
                latestWave = StageWaves[nWave];
            }
        }

        return latestWave;
    }
}