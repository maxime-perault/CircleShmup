using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Stores informations about an enemy spawner
 * @class SpawnerData
 */
[CreateAssetMenu(fileName = "SpawnerData", menuName = "Shmup/Spawner Data")]
public class SpawnerData : ScriptableObject
{
    public string     SpawnerName;
    public GameObject SpawnerPrefab;

    public int               SpawnerSpawnCount;
    public List<float>       SpawnerSpawnTiming    = new List<float>();
    public List<Transform>   SpawnerSpawnPositions = new List<Transform>();
}