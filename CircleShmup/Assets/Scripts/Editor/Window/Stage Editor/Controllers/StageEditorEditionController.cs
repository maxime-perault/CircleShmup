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
     * TODO
     */
    public static void AddWave(List<Wave> waves)
    {
        waves.Add(new Wave());
    }

    /**
     * TODO
     */
    public static void RemoveWave(List<Wave> waves, Wave wave)
    {
        if (waves.Contains(wave))
        {
            waves.Remove(wave);
        }
    }

    /**
     * TODO
     */
    public static void RemoveAll(List<Wave> waves)
    {
        waves.Clear();
    }
}
