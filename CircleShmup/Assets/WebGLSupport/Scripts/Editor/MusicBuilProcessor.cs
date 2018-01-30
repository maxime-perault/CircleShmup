#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEditor.Build;

/**
 * Called before the build to excluse with from build if needed
 * @class MusicBuildProcessor
 */
class MusicBuildProcessor : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }

    /**
     * TODO
     */
    public void OnPreprocessBuild(BuildTarget target, string path)
    {
        if(target.ToString() == "WebGL")
        {
            // AssetDatabase.MoveAsset("Wwise", "~Wwise");
        }
    }
}

#endif