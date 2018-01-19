using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores two attributes to make a list sort possible
 * @class SpawnInfo
 */
[System.Serializable]
public class SpawnInfo
{
    [SerializeField] public GameObject SpawnPrefab   = null;
    [SerializeField] public float      SpawnTiming   = 0.0f;
    [SerializeField] public Vector3    SpawnPosition = new Vector3(0.0f, 0.0f, 0.0f);
    
    /**
     * Comparisons helper
     */
    public int CompareTo(SpawnInfo other)
    {
        if(SpawnTiming > other.SpawnTiming)  return 1;
        if(SpawnTiming == other.SpawnTiming) return 0;

        return -1;
    }
}

/**
 * Stores informations about an enemy spawner
 * @class SpawnerData
 */
[System.Serializable]
[CreateAssetMenu(fileName = "SpawnerData", menuName = "Shmup/Spawner Data")]
public class SpawnerData : ScriptableObject
{
    [SerializeField]  public string          SpawnerName;
    [SerializeField]  public int             SpawnerSpawnCount;
    [SerializeField]  public List<SpawnInfo> SpawnerInfo = new List<SpawnInfo>();

    /**
     * Returns the latest timing 
     * @return The latest timing
     */
    public float GetLastTiming()
    {
        int timingCount = SpawnerInfo.Count;

        if(timingCount == 0)
        {
            return 0.0f;
        }

        float lastTiming = SpawnerInfo[0].SpawnTiming;

        for (int nTiming = 0; nTiming < timingCount; ++nTiming)
        {
            if (SpawnerInfo[nTiming].SpawnTiming > lastTiming)
            {
                lastTiming = SpawnerInfo[nTiming].SpawnTiming;
            }
        }

        return lastTiming;
    }

    /**
     * Sorts data by timing (ascending)
     */
    public void SortSpawnerInfo()
    {
        System.Comparison<SpawnInfo> comparer = (x, y) => x.CompareTo(y);
        SpawnerInfo.Sort(comparer);
    }

    /**
     * Avoid object reset when the scene change or after play/stop
     */
    private void OnDisable()
    {
        EditorUtility.SetDirty(this);
    }
}