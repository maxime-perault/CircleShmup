using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom editor to create enemy patterns
 * @class PatternEditorController
 */
public class PatternEditorController
{
    /**
     * Initializes the editor window
     */
    [MenuItem("Shmup/Pattern Editor")]
    static void Init()
    {
        PatternEditorNodeView.OpenPatternEditorNodeView();
    } 
}
