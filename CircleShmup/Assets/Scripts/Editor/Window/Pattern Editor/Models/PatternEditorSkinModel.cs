using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Models for all node skins stuff
 * @class PatternEditorSkinModel
 */
public class PatternEditorSkinModel
{
    public static GUIStyle styleNode; 
    public static GUIStyle styleInPoint;
    public static GUIStyle styleOutPoint;
    public static GUIStyle styleSelectedNode;

    /**
     * Loads all node skins (darkskin)
     */
    public static void LoadSkins()
    {
        styleNode        = new GUIStyle();
        styleNode.border = new RectOffset(12, 12, 12, 12);
        styleNode.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        
        styleInPoint        = new GUIStyle();
        styleInPoint.border = new RectOffset(4, 4, 12, 12);
        styleInPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png")    as Texture2D;
        styleInPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        
        styleOutPoint        = new GUIStyle();
        styleOutPoint.border = new RectOffset(4, 4, 12, 12);
        styleOutPoint.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png")    as Texture2D;
        styleOutPoint.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        
        styleSelectedNode        = new GUIStyle();
        styleSelectedNode.border = new RectOffset(12, 12, 12, 12);
        styleSelectedNode.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;   
    }
}
