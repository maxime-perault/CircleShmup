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
        LoadDatabase();
        if(databaseLoaded)
        {
            StageEditorView.Init();
        }
    }

    /**
     * Displays the creation menu
     */
    public static void CreateStage()
    {
        StageEditorView.editionView.ToogleCreation();
    }

    /**
     * Enables the edition of the given stage
     * @param stage The stage to edit
     */
    public static void EditStage(Stage stage)
    {
        StageEditorView.editionView.ToogleEdition(stage);
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
     * Updates a stage from another one
     * @param old   The old stage
     * @param other The new stage
     */
    public static void UpdateStage(Stage old, Stage other)
    {
        List<Stage> stages = StageEditorModel.databaseInstance.stages;
        if (stages.Contains(old))
        {
            int index = stages.IndexOf(old);
            stages[index] = other;
        }
    }

    /**
     * Adds a new stage in the database
     * @param stage The stage to add in the database
     */
    public static void AddStage(Stage stage)
    {
        List<Stage> stages = StageEditorModel.databaseInstance.stages;
        stages.Add(stage);
    }

    /**
     * Loads the database
     */
    public static bool LoadDatabase()
    {
        if(!StageEditorModel.databaseInstance)
        {
            databaseLoaded = StageEditorModel.LoadStageDatabase();
            return true;
        }

        return false;
    }
}
