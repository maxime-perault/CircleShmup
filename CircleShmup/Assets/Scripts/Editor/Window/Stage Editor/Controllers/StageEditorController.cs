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
}
