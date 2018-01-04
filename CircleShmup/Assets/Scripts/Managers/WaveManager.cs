using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Manages enemy waves
 * @class WaveManager
 */
public class WaveManager
{
    public enum ManagerState
    {
        ManagerNone,
        ManagerBegin,
        ManagerRunning,
        ManagerDone
    }

    private float timer;
    private Transform    parent;
    private ManagerState managerState = ManagerState.ManagerNone;

    private Stage            currenteStage = null;
    private List<int>        waveIndexes   = new List<int>();
    private List<WaveHandle> currentWaves  = new List<WaveHandle>();
  
    /**
     * Initializes the wave manager with the stage to process
     * @param stage The stage to process
     */
    public void Init(Stage stage, Transform parent)
    {
        waveIndexes.Clear();
        currentWaves.Clear();

        timer         = 0.0f;
        this.parent   = parent;
        currenteStage = stage;
        managerState  = ManagerState.ManagerBegin;

        int waveCount = currenteStage.StageWaves.Count;
        for (int nWave = 0; nWave < waveCount; ++nWave)
        {
            waveIndexes.Add(nWave);
        }
    }

    /**
     * Updates the wave manager
     * @param dt The elasped time
     */
    public void Update(float dt)
    {
        timer += dt;

        // Manager switch state machine
        switch(managerState)
        {
            case ManagerState.ManagerBegin:   OnManagerBegin();   break;
            case ManagerState.ManagerRunning: OnManagerRunning(); break;
            default: break;
        }
    }

    /**
     * Called when the manager begins
     */
    private void OnManagerBegin()
    {
        List<int> firstWaves = GetFirstWaves();
        if (firstWaves.Count == 0)
        {
            managerState = ManagerState.ManagerDone;
            return;
        }

        if(timer < currenteStage.StageWaves[firstWaves[0]].WaveTiming)
        {
            return;
        }

        GameObject oiginalHandle = Resources.Load("Prefabs/WaveHandle") as GameObject;

        // Creating handles
        Debug.Log("Wave Manager : ");
        for (int nWave = 0; nWave < firstWaves.Count; ++nWave)
        {
            GameObject go = UnityEngine.GameObject.Instantiate(oiginalHandle, parent);
            WaveHandle handle = go.GetComponent<WaveHandle>();

            Wave wave = currenteStage.StageWaves[firstWaves[nWave]];
            handle.Init(wave, firstWaves[nWave]);
            currentWaves.Add(handle);

            Debug.Log("    - Wave " + wave.WaveName + " loaded.");
        }

        managerState = ManagerState.ManagerRunning;
    }

    /**
     * Called when the manager is running
     */
    private void OnManagerRunning()
    {
        bool allDone = true;
        for(int nHandle = 0; nHandle < currentWaves.Count; ++nHandle)
        {
            if(currentWaves[nHandle].GetState() == WaveHandle.WaveState.WaveRunning)
            {
                allDone = false;
                break;
            }
        }

        if(allDone)
        {
            // Removes old index
            for (int nHandle = 0; nHandle < currentWaves.Count; ++nHandle)
            {
                waveIndexes.Remove(currentWaves[nHandle].waveIndex);
                UnityEngine.GameObject.Destroy(currentWaves[nHandle].gameObject);
            }

            currentWaves.Clear();
            managerState = ManagerState.ManagerBegin;

            Debug.Log("Wave Manager : Wave(s) done");
        }
    }

    /**
     * Returns the state of the manager
     * @return managerState
     */
    public ManagerState GetManagerState()
    {
        return managerState;
    }

    /**
     * Returns the first waves to execute
     * from the indexes list
     * @return a list of index
     */
    private List<int> GetFirstWaves()
    {
        List<int> firstIndexes = new List<int>();
        List<Wave> waves       = currenteStage.StageWaves;

        int waveIndexesCount = waveIndexes.Count;

        // Pathologic cases
        if (waveIndexesCount == 0)
        {
            return firstIndexes;
        }
        else if (waveIndexesCount == 1)
        {
            firstIndexes.Add(waveIndexes[0]);
            return firstIndexes;
        }

        firstIndexes.Add(waveIndexes[0]);
        for (int nIndex = 1; nIndex < waveIndexesCount; ++nIndex)
        {
            if (waves[firstIndexes[0]].WaveTiming > waves[waveIndexes[nIndex]].WaveTiming)
            {
                firstIndexes.Clear();
                firstIndexes.Add(waveIndexes[nIndex]);
            }
            // There is another one wave with the same timing, we must consider
            else if (waves[firstIndexes[0]].WaveTiming == waves[waveIndexes[nIndex]].WaveTiming)
            {
                firstIndexes.Add(waveIndexes[nIndex]);
            }
        }

        return firstIndexes;
    }
}