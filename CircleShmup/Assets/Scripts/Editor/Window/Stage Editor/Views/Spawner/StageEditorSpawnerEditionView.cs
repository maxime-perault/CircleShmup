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

    private int          spawnerCountBuffer;
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
        window.maxSize = new Vector2(600, 500);
        window.minSize = window.maxSize;
        window.Show();

        if(data != null)
        {
            window.spawnerData = data;
            window.editorState = EditionState.Edition;
            window.spawnerCountBuffer = data.SpawnerInfo.Count;

            window.spawnerData.SortSpawnerInfo();
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
        GUILayout.BeginArea(new Rect(20, 20, 560, 80));

        spawnerData.SpawnerName       = EditorGUILayout.TextField  ("Spawner name",  spawnerData.SpawnerName);
        spawnerCountBuffer            = EditorGUILayout.IntField   ("Enemy count",   spawnerCountBuffer);
        spawnerData.SpawnerPrefab     = EditorGUILayout.ObjectField("Spawner enemy", spawnerData.SpawnerPrefab, typeof(GameObject), true) as GameObject;

        spawnerData.SpawnerSpawnCount = spawnerCountBuffer;

        GUILayout.EndArea();

        List<SpawnInfo> infos = spawnerData.SpawnerInfo;
        int diff = spawnerData.SpawnerSpawnCount - infos.Count;

        if(diff > 0)
        {
            for (int nData = 0; nData < diff; ++nData)
            {
                infos.Add(new SpawnInfo());
            }
        }

        GUILayout.BeginArea(new Rect(20, 100, 560, 350));
        scrollBarPosition = GUILayout.BeginScrollView(scrollBarPosition, false, true, GUIStyle.none, GUI.skin.verticalScrollbar);

        // Displays all 
        for (int nData = 0; nData < spawnerData.SpawnerSpawnCount; ++nData)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                infos.RemoveAt(nData);

                spawnerCountBuffer            -= 1;
                spawnerData.SpawnerSpawnCount -= 1;

                return;
            }

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Timing", GUILayout.Width(45));
            infos[nData].SpawnTiming   = EditorGUILayout.FloatField(infos[nData].SpawnTiming, GUILayout.Width(50));

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Position", GUILayout.Width(55));
            infos[nData].SpawnPosition = EditorGUILayout.Vector3Field("", infos[nData].SpawnPosition, GUILayout.Width(120));

            GUILayout.Space(20);
            Transform selected = null;
            selected = EditorGUILayout.ObjectField(selected, typeof(Transform), true, GUILayout.Width(100)) as Transform;

            if(selected != null)
            {
                infos[nData].SpawnPosition = selected.position;

            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(20, 470, 560, 30));
        if (editorState == EditionState.Creation)
        {
            if (GUILayout.Button("Create"))
            {
                // Apply list modifications
                diff = spawnerData.SpawnerSpawnCount - infos.Count;

                if (diff < 0)
                {
                    for(int nData = 0; nData < (diff * -1); ++nData)
                    {
                        infos.RemoveAt(infos.Count - 1);
                    }
                }

                spawnerData.SortSpawnerInfo();
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