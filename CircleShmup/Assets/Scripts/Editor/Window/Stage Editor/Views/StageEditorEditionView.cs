using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Displays all stages preview
 * @class StageEditorEditionView
 */
public class StageEditorEditionView
{
    public enum EditionState
    {
        Edition,
        Creation
    }

    private EditionState State;
    private Stage        SourceStage;
    private Stage        CurrentStage;
    private Vector2      scrollBarPosition;

    /**
     * Constructor. Sets the state to Creation
     */
    public StageEditorEditionView()
    {
        ToogleCreation();
    }

    /**
     * Puts the editor in creation mode
     */
    public void ToogleCreation()
    {
        SourceStage    = null;
        CurrentStage   = new Stage();

        State = EditionState.Creation;
    }

    /**
     * Puts the editor in edition mode
     */
    public void ToogleEdition(Stage source)
    {
        SourceStage  = source;
        CurrentStage = new Stage(source);

        State = EditionState.Edition;
    }

    /**
     * Displays the edition area
     */
    public void OnGUI()
    {
        // Displays stage properties
        EditorGUILayout.BeginHorizontal();
        GUILayout.BeginArea(new Rect(420, 10, 560, 250));
        GUILayout.Label("Edition", EditorStyles.boldLabel);

        GUILayout.Space(10);
        EditorGUILayout.LabelField("General");
        GUILayout.Space(5);

        // Displays basic stage informations
        CurrentStage.StageID          = (uint)EditorGUILayout.IntField  ("Stage ID",     (int)CurrentStage.StageID,          GUILayout.Width(530));
        CurrentStage.StageName        =       EditorGUILayout.TextField ("Stage name",        CurrentStage.StageName,        GUILayout.Width(530));
        CurrentStage.StageDescription =       EditorGUILayout.TextField ("Stage description", CurrentStage.StageDescription, GUILayout.Width(530));
        CurrentStage.StageTimeout     =       EditorGUILayout.FloatField("Stage timeout",     CurrentStage.StageTimeout,     GUILayout.Width(530));

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Waves");
        GUILayout.Space(5);

        // Display stage waves informations
        List<Wave> waves = CurrentStage.StageWaves;
        EditorGUILayout.LabelField("Waves count", waves.Count.ToString(), GUILayout.Width(210));

        GUILayout.Space(20);
        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Wave",   GUILayout.Width(270))) { StageEditorEditionController.AddWave(waves);   }
        if (GUILayout.Button("Remove all", GUILayout.Width(270))) { StageEditorEditionController.RemoveAll(waves); }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Spawner", GUILayout.Width(270))) { StageEditorEditionController.CreateSpawner(); }
        if (GUILayout.Button("Edit Spawner", GUILayout.Width(270)))   { StageEditorEditionController.EditSpawner();   }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        GUILayout.EndArea();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.BeginArea(new Rect(420, 260, 560, 300));

        scrollBarPosition = GUILayout.BeginScrollView(scrollBarPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);

        int waveCount = waves.Count;
        for(int nWave = 0; nWave < waveCount; ++nWave)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Remove", GUILayout.Width(140)))
            {
                StageEditorEditionController.RemoveWave(waves, waves[nWave]);
                return;
            }
            EditorGUILayout.EndHorizontal();

            waves[nWave].WaveID       = (uint)EditorGUILayout.IntField   ("Wave ID",  (int)waves[nWave].WaveID);
            waves[nWave].WaveName     =       EditorGUILayout.TextField  ("Wave name",     waves[nWave].WaveName);
            waves[nWave].WaveEnemy    =       EditorGUILayout.ObjectField("Wave enemy",    waves[nWave].WaveEnemy, typeof(GameObject), true) as GameObject;
            waves[nWave].WaveTiming   =       EditorGUILayout.FloatField ("Wave timing",   waves[nWave].WaveTiming);
            waves[nWave].WaveDuration =       EditorGUILayout.FloatField ("Wave duration", waves[nWave].WaveDuration);

            GUILayout.Space(20);
            GUILayout.Box("", GUILayout.Width(535), GUILayout.Height(1));
            GUILayout.Space(20);
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
        EditorGUILayout.EndHorizontal();

        GUILayout.BeginArea(new Rect(420, 570, 560, 50));
        if (State == EditionState.Creation)
        {
            if (GUILayout.Button("Create"))
            {
                StageEditorController.AddStage(CurrentStage);
                ToogleCreation();
            }
        }
        else
        {
            if (GUILayout.Button("Update"))
            {
                StageEditorController.UpdateStage(SourceStage, CurrentStage);
                ToogleCreation();
            }
        }
        GUILayout.EndArea();

    }
}