using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom editor to create and sequence game stages
 * @class StageEditorController
 */
public class StageEditorController
{
    private static bool databaseLoaded = false;

    /**
     * Initializes the editor window. Loads the stage database
     */
    [MenuItem("Shmup/Stage Editor")]
    static void Init()
    {
        databaseLoaded = StageEditorModel.LoadStageDatabase();

        if(databaseLoaded)
        {
            StageEditorView.Init();
        }
    }

    /**
     * Removes a stage from the database. Rebuilds index.
     * @param stage The stage to remove
     */
    public static void RemoveStage(Stage stage)
    {
        List<Stage> stages = StageEditorModel.databaseInstance.stages;

        if (stages.Contains(stage))
        {
            stages.Remove(stage);
        }
    }

    /**
     * Removes all entries from the database
     */
    public static void RemoveAllStages()
    {
        List<Stage> stages = StageEditorModel.databaseInstance.stages;
        stages.Clear();
    }
    
    /**
     * Toogles the creation menu
     */
    public static void CreateStage()
    {
        // TODO
    }

    /**
     * Adds a new stage in the database
     * @param stage The stage to add in the database
     */
    public static void AddEntry(Stage stage)
    {
        List<Stage> stages = StageEditorModel.databaseInstance.stages;
        stages.Add(stage);
    }
}
