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
}