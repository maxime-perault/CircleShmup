using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom editor window to edite spawner
 * @class StageEditorSpawnerEditionView
 */
public class StageEditorSpawnerEditionView : EditorWindow
{
    public enum EditionState
    {
        Edition,
        Creation
    }

    private Vector2      scrollBarPosition;
    private SpawnerData  spawnerData  = null;
    private EditionState editorState = EditionState.Creation;

    /**
     * Initializes the editor window
     */
    public static void Init(SpawnerData data)
    {
        StageEditorSpawnerEditionView window = EditorWindow.GetWindow<StageEditorSpawnerEditionView>();

        window.titleContent.text = "Spawner Editor";
        window.maxSize = new Vector2(500, 500);
        window.minSize = window.maxSize;
        window.Show();

        if(data != null)
        {
            window.spawnerData = data;
            window.editorState = EditionState.Edition;
        }
        else
        {
            window.spawnerData = ScriptableObject.CreateInstance<SpawnerData>();
        }
    }

    /**
     * Renders the entire editor window from helper class
     */
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 460, 80));

        spawnerData.SpawnerName       = EditorGUILayout.TextField  ("Spawner name",  spawnerData.SpawnerName);
        spawnerData.SpawnerSpawnCount = EditorGUILayout.IntField   ("Enemy count",   spawnerData.SpawnerSpawnCount);
        spawnerData.SpawnerPrefab     = EditorGUILayout.ObjectField("Spawner enemy", spawnerData.SpawnerPrefab, typeof(GameObject), true) as GameObject;

        GUILayout.EndArea();

        List<float> timings       = spawnerData.SpawnerSpawnTiming;
        List<Transform> positions = spawnerData.SpawnerSpawnPositions;

        int diff = spawnerData.SpawnerSpawnCount - timings.Count;

        if (diff < 0)
        {
            for(int nData = 0; nData < (diff * -1); ++nData)
            {
                timings.RemoveAt(timings.Count - 1);
                positions.RemoveAt(positions.Count - 1);
            }
        }
        else if(diff > 0)
        {
            for (int nData = 0; nData < diff; ++nData)
            {
                timings.Add(0.0f);
                positions.Add(null);
            }
        }

        GUILayout.BeginArea(new Rect(20, 100, 460, 350));
        scrollBarPosition = GUILayout.BeginScrollView(scrollBarPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);

        // Displays all 
        for (int nData = 0; nData < timings.Count; ++nData)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                // TODO
                return;
            }

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Timing", GUILayout.Width(45));
            timings[nData]   = EditorGUILayout.FloatField(timings[nData], GUILayout.Width(50));

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Position", GUILayout.Width(55));
            positions[nData] = EditorGUILayout.ObjectField(positions[nData], typeof(Transform), true, GUILayout.Width(150)) as Transform;

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(20, 470, 460, 30));
        if (editorState == EditionState.Creation)
        {
            if (GUILayout.Button("Create"))
            {
                StageEditorSpawnerController.SaveAsset(spawnerData);
                Close();
            }
        }
        else
        {
            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }
        GUILayout.EndArea();
    }
}