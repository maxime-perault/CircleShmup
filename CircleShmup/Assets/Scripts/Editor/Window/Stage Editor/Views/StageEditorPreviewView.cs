using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Displays all stages preview
 * @class StageEditorPreviewView
 */
public class StageEditorPreviewView
{
    private Vector2 scrollBarPosition;

    /**
     * Called to draw the preview
     */
    public void OnGUI()
    {
        GUILayout.Label("Stages", EditorStyles.boldLabel);
        GUILayout.BeginArea(new Rect(10, 25, 400, 600));

        EditorGUILayout.BeginHorizontal();

        scrollBarPosition = EditorGUILayout.BeginScrollView(scrollBarPosition, false, true, 
            GUILayout.ExpandWidth(true), 
            GUILayout.ExpandHeight(true));

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("New stage"))  { /*EditorMonsterDatabaseWindow.CreateMonster();*/ }
        if (GUILayout.Button("Remove all")) { StageEditorController.RemoveAllStages();    }
 
        GUILayout.Space(20);
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(30);
        DisplayAllStagePreviews();

        EditorGUILayout.EndToggleGroup();
        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    /**
     * Displays all stages from the model stage list
     */
    private void DisplayAllStagePreviews()
    {
        // Getting the list of stages from the model
        List<Stage> stages = StageEditorModel.databaseInstance.stages;

        if(stages == null)
        {
            EditorUtility.DisplayDialog(
                "Stages list null reference",
                "Unable to initializes the refernce for the stages list", "Ok");

            return;
        }

        int stageCount = stages.Count;
        for(int nStage = 0; nStage < stageCount; ++nStage)
        {
            Stage stage = stages[nStage];
            EditorGUILayout.LabelField("Stage ID :",          stage.StageID.ToString());
            EditorGUILayout.LabelField("Stage name :",        stage.StageName);
            EditorGUILayout.LabelField("Stage description :", stage.StageDescription);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Edit",   GUILayout.Width(185))) { /*EditorMonsterDatabaseWindow.Edit(MonsterItem);*/   }
            if (GUILayout.Button("Remove", GUILayout.Width(185))) { StageEditorController.RemoveStage(stage); }

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);
            GUILayout.Box("", GUILayout.Width(370), GUILayout.Height(1));
            GUILayout.Space(20);
        }    
    }
}
