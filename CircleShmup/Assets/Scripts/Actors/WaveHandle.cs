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

    public Wave      wave;
    public WaveState waveState;
    public int       waveIndex;

    private float       elapsedTime;
    private SpawnerData spawnerData;
    private IEnumerator spawnerCoroutine;
    private Transform   parentTransform;

    private int         enemyLeft;

    /**
     * Initializes the wave handle
     * @param wave The wave to process
     * @param index The current index of the handle
     */
    public void Init(Wave wave, int index)
    {
        this.wave    = wave;
        waveIndex    = index;
        waveState    = WaveState.WaveRunning;
        spawnerData  = wave.WaveSpawner;

        if(spawnerData == null)
        {
            waveState = WaveState.WaveEnd;
            return;
        }

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
        elapsedTime += Time.deltaTime;

        if(enemyLeft == 0)
        {
            waveState = WaveState.WaveEnd;
        }
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
        int currentIndex     = 0;
        List<SpawnInfo> info = spawnerData.SpawnerInfo;

        while(currentIndex != spawnerData.SpawnerInfo.Count)
        {
            yield return new WaitForSeconds(info[currentIndex].SpawnTiming - elapsedTime);

            // We can spawn a new enemy
            GameObject enemy         = Instantiate(spawnerData.SpawnerPrefab, parentTransform);
            enemy.transform.position = info[currentIndex].SpawnPosition.position;

            // Getting the enemy script attached to the game object
            // to make any feeback possible
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.handle = this;

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
     * TODO
     */
    public void OnEnemyDeath()
    {
        Debug.Log("EnemyLeft--");
        enemyLeft--;
    }
}