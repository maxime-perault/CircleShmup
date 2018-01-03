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
    [SerializeField] public uint       WaveID;
    [SerializeField] public string     WaveName;
    [SerializeField] public GameObject WaveEnemy;
    [SerializeField] public int        WaveEnemyCount;
    [SerializeField] public float      WaveDuration;

    /**
     * Default constructor
     */
    public Wave()
    {
        WaveID         = 0;
        WaveName       = "Unknown";
        WaveEnemy      = null;
        WaveEnemyCount = 0;
        WaveDuration   = 0.0f;
    }

    /**
     * Copy constructor
     */
    public Wave(Wave other)
    {
        WaveID         = other.WaveID;
        WaveName       = other.WaveName;
        WaveEnemy      = other.WaveEnemy;
        WaveEnemyCount = other.WaveEnemyCount;
        WaveDuration   = other.WaveDuration;
    }

    /**
     * Constructs a Wave from parameters list 
     */
    public Wave(uint id, string name, GameObject enemy, int count, float duration)
    {
        WaveID         = id;
        WaveName       = name;
        WaveEnemy      = enemy;
        WaveEnemyCount = count;
        WaveDuration   = duration;
    }
}
