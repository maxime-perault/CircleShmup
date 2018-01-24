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
        WaveBlocking,
        WaveEnd
    }

    public bool      paused;
    public Wave      wave;
    public WaveState waveState;
    public int       waveIndex;

    private float       elapsedTime;
    private SpawnerData spawnerData;
    private IEnumerator spawnerCoroutine;
    private Transform   parentTransform;
    private List<Enemy> enemyReferenceBuffer = new List<Enemy>();

    private int         enemyLeft;
    private int         currentIndex;

    /**
     * Initializes the wave handle
     * @param wave The wave to process
     * @param index The current index of the handle
     */
    public void Init(Wave wave, int index)
    {
        paused       = false;
        this.wave    = wave;
        waveIndex    = index;
        waveState    = WaveState.WaveRunning;
        spawnerData  = wave.WaveSpawner;

        if(spawnerData == null)
        {
            waveState = WaveState.WaveEnd;
            return;
        }

        currentIndex = 0;
        spawnerCoroutine = SpawnEnemies();
        StartCoroutine(spawnerCoroutine);

        enemyLeft = spawnerData.SpawnerSpawnCount;
        parentTransform = GameObject.Find("Enemies").transform;
    }

    /**
     * Updates the wave handle
     */
    void Update()
    {
        if(paused)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if(enemyLeft == 0)
        {
            waveState = WaveState.WaveEnd;
        }
    }

    /**
     * Called when the game is paused
     */
    public void OnGamePaused()
    {
        int referenceCount = enemyReferenceBuffer.Count;
        for(int nReference = 0; nReference < referenceCount; ++nReference)
        {
            if (enemyReferenceBuffer[nReference] != null)
            {
                enemyReferenceBuffer[nReference].OnGamePaused();
            }
        }

        paused = true;
        StopAllCoroutines();
    }

    /**
     * Called when the game resumes
     */
    public void OnGameResumed()
    {
        int referenceCount = enemyReferenceBuffer.Count;
        for (int nReference = 0; nReference < referenceCount; ++nReference)
        {
            if(enemyReferenceBuffer[nReference] != null)
            {
                enemyReferenceBuffer[nReference].OnGameResumed();
            }
        }

        paused = false;
        StartCoroutine(spawnerCoroutine);
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
     * Usefull coroutines that instantiates all enemy depending
     * their timing and their start positions
     */
    public IEnumerator SpawnEnemies()
    {
        List<SpawnInfo> info = spawnerData.SpawnerInfo;
        while(currentIndex != spawnerData.SpawnerInfo.Count)
        {
            yield return new WaitForSeconds(info[currentIndex].SpawnTiming - elapsedTime);

            // We can spawn a new enemy
            GameObject enemy         =  Instantiate(info[currentIndex].SpawnPrefab, parentTransform);
            enemy.transform.position = info[currentIndex].SpawnPosition;

            // Getting the enemy script attached to the game object
            // to make any feeback possible
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            enemyScript.handle      = this;
            enemyScript.bufferIndex = enemyReferenceBuffer.Count;

            // Buffers the enemy
            enemyReferenceBuffer.Add(enemyScript);

            currentIndex++;
        }

        if(!wave.WaveBlocking || enemyLeft == 0)
        {
            waveState = WaveState.WaveEnd;
        }
        else
        {
            waveState = WaveState.WaveBlocking;
        }
    }

    /**
     * Called when an enemy is dead
     */
    public void OnEnemyDeath(int bufferIndex)
    {
        enemyLeft--;
        enemyReferenceBuffer[bufferIndex] = null;
    }
}