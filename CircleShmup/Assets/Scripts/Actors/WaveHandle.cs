using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Class to process waves independently
 * @class WaveHandle
 */
public class WaveHandle : MonoBehaviour
{
    public enum WaveState
    {
        WaveBegin,
        WaveRunning,
        WaveEnd
    }

    private IEnumerator coroutine;
    private Wave        wave;

    public WaveState waveState;
    public int       waveIndex;
    public float     waveDuration;

    public void Init(Wave wave, int index)
    {
        this.wave    = wave;
        waveIndex    = index;
        waveState    = WaveState.WaveRunning;
        waveDuration = wave.WaveDuration;

        coroutine = WaitDuration();
        StartCoroutine(coroutine);
    }

    /**
     * Returns the current state of the handle
     * @return The state of the handle
     */
    public WaveState GetState()
    {
        return waveState;
    }

    /**
     * Tool coroutines
     */
    private IEnumerator WaitDuration()
    {
        yield return new WaitForSeconds(wave.WaveDuration);
        waveState = WaveState.WaveEnd;
    }
}