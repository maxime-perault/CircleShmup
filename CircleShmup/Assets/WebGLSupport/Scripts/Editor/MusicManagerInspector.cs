using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for the music manager
 * @class MusicManagerInspector
 */
[CustomEditor(typeof(MusicManager))]
public class MusicManagerInspector : Editor
{
    public bool WwiseMoved = false;

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

            if(musicManager.WebGLBuild && !WwiseMoved)
            {
                // ExcludeWwise();
                WwiseMoved = true;
            }
            else if(!musicManager.WebGLBuild && WwiseMoved)
            {
                // TODO
                WwiseMoved = false;
            }

#endif

        #if USE_WWISE
            Debug.Log("");
        #endif

        // Editor bug fix with custom window
        EditorUtility.SetDirty(musicManager);
    }

    /**
     * Helper method to exclude Wwise
     */
    public static void ExcludeWwise()
    {
        int index      = (Application.dataPath  + "/Wwise").IndexOf("Wwise") + 6;
        var root       = Application.dataPath.Replace("Assets", "");
        var exclude    = root + "Exclude/";
        var sourcePath = Application.dataPath + "/Wwise";
        var targetPath = exclude;

        // Gets all files to move
        var files = System.IO.Directory.GetFiles(sourcePath, "*", System.IO.SearchOption.AllDirectories);

        // Check dest folders
        if(!System.IO.Directory.Exists(exclude))
        {
            System.IO.Directory.CreateDirectory(exclude);
        }

        // Moves all files
        foreach(string path in files)
        {
            string source_path = path.Replace('\\', '/');
            string target_path = exclude + source_path.Substring(index, path.Length - index);

            if(System.IO.File.Exists(target_path))
            {
                System.IO.File.Delete(target_path);
            }

            int lastIndex    = target_path.LastIndexOf('/');
            string directory = target_path.Substring(0, lastIndex);
            Debug.Log("CDir : " + target_path);
            Debug.Log("Dir : " + directory);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            System.IO.File.Move(source_path, target_path);
            
        }
    }
}