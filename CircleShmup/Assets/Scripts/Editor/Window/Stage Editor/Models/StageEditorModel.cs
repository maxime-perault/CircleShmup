using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Data model of the stage editor
 * @class StageEditorModel
 */
public class StageEditorModel
{
    static public StageDatabase databaseInstance;

    /**
     * Initializes the editor window. Loads the stage database
     * @return False on loading error, else true
     */
    public static bool LoadStageDatabase()
    {
        databaseInstance = (StageDatabase)Resources.Load("Databases/StageDatabase") as StageDatabase;
        if (!databaseInstance)
        {
            EditorUtility.DisplayDialog(
                "Unable to load the stages database",
                "Please check if the database exist in the resources folder.", "Ok");

            return false;
        }

        return true;
    }
}
