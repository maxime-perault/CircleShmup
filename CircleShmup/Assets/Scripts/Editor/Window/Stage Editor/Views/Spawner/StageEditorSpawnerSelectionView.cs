using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom editor window to select a spawner to edit
 * @class StageEditorSpawnerSelectionView
 */
public class StageEditorSpawnerSelectionView : EditorWindow
{
    private SpawnerData spawnerData = null;

    /**
     * Initializes the editor window
     */
    public static void Init()
    {
        StageEditorSpawnerSelectionView window = EditorWindow.GetWindow<StageEditorSpawnerSelectionView>();

        window.titleContent.text = "Spawner Selector";
        window.maxSize = new Vector2(250, 80);
        window.minSize = window.maxSize;
        window.Show();
    }

    /**
     * 
     */
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 230, 60));

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Spawner", GUILayout.Width(60)); GUILayout.Space(10);
        spawnerData = EditorGUILayout.ObjectField(spawnerData, typeof(SpawnerData), false) as SpawnerData;
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(20);
        if (GUILayout.Button("Edit"))
        {
            StageEditorSpawnerEditionView.Init(spawnerData);
            Close();
        }

        GUILayout.EndArea();
    }
}