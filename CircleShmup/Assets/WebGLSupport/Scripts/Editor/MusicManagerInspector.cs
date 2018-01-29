using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for the music manager
 * @class MusicManagerInspector
 */
[CustomEditor(typeof(MusicManager))]
public class MusicManagerInspector : Editor
{
    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Buffering info
        MusicManager musicManager = (MusicManager)target;

        #if UNITY_EDITOR

            // Enable audio ...
            var settings = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/AudioManager.asset")[0];
            var serializedManager = new SerializedObject(settings);
            var property = serializedManager.FindProperty("m_DisableAudio");

            // Toogles the boolean
            property.boolValue = !musicManager.WebGLBuild;
            serializedManager.ApplyModifiedProperties();

        #endif

        // Editor bug fix with custom window
        EditorUtility.SetDirty(musicManager);
    }
}