using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Controller for the edition view
 * @class StageEditorEditionController
 */
public class StageEditorEditionController
{
    /**
     * Adds a new wave into the wave list
     * @param waves The list of waves
     */
    public static void AddWave(List<Wave> waves)
    {
        waves.Add(new Wave());
    }

    /**
     * Removes a wave from the waves list
     * @param waves The list of waves
     * @param wave The wave to remove
     */
    public static void RemoveWave(List<Wave> waves, Wave wave)
    {
        if (waves.Contains(wave))
        {
            waves.Remove(wave);
        }
    }

    /**
     * Removes all entry in the waves list
     * @param waves The list of waves to clear
     */
    public static void RemoveAll(List<Wave> waves)
    {
        waves.Clear();
    }

    /**
     * Toogles the spawner creation window
     */
    public static void CreateSpawner()
    {
        StageEditorSpawnerEditionView.Init(null);
    }

    /**
     * Toogles the spawner edition window
     */
    public static void EditSpawner()
    {
        StageEditorSpawnerController.OpenSelectionWindow();
    }
}
