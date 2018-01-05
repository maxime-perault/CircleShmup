using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Controller for the spawner edition view
 * @class StageEditorSpawnerController
 */
public class StageEditorSpawnerController
{
    /**
     * TODO
     */
    public static void OpenSelectionWindow()
    {
        StageEditorSpawnerSelectionView.Init();
    }

    /**
     * Saves the spawner data into the disk
     * @param data The data to save
     */
    public static void SaveAsset(SpawnerData data)
    {
        AssetDatabase.CreateAsset(data, "Assets/Resources/Databases/Spawners/" + data.SpawnerName + ".asset");
    }
}
