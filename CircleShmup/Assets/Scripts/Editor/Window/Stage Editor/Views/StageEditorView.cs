using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom editor window to create game stages
 * @class StageEditorView
 */
public class StageEditorView : EditorWindow
{
    static private StageEditorPreviewView previewView = new StageEditorPreviewView();

    /**
     * Initializes the editor window. Loads the monster database asset.
     */
    public static void Init()
    {
        StageEditorView window = (StageEditorView)EditorWindow.GetWindow(typeof(StageEditorView));

        window.titleContent.text = "Stage Editor";
        window.maxSize = new Vector2(1000, 600);
        window.minSize = window.maxSize;
        window.Show();
    }

    /**
     * Renders the entire editor window from helper class
     */
    void OnGUI()
    {
        previewView.OnGUI();
    }

}