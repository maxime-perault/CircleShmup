using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about all game stages
 * @class StageDatabase
 */
[CreateAssetMenu(fileName = "StageDatabase", menuName = "Shmup/Stage Database")]
public class StageDatabase : ScriptableObject
{
    public List<Stage> stages = new List<Stage>();

    /**
     * Returns the total count of waves
     * @return The count of waves
     */
    public int GetTotalWavesCount()
    {
        int waveCount  = 0;
        int stageCount = stages.Count;
        for (int nStage = 0; nStage < stageCount; ++nStage)
        {
            waveCount += stages[nStage].StageWaves.Count;
        }

        return waveCount;
    }

    /**
     * Returns the sum of all stage duration
     * @return The total duration of the game
     */
    public float GetTotalDuration()
    {
        float duration = 0.0f;
        int stageCount = stages.Count;
        for (int nStage = 0; nStage < stageCount; ++nStage)
        {
            duration += stages[nStage].GetStageDuration();
        }

        return duration;
    }
}