using UnityEngine;
using UnityEditor;

/**
 * Helpers methods
 * @class InspectorHelper
 */
public class InspectorHelper
{
    /**
     * Displays in the inspector a simple line with the specified title
     * @title The title of the seperator
     */
    public static void DisplaySeparator(string title)
    {
        GUILayout.Label(title, EditorStyles.boldLabel);
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
    }

    /**
     * Displays a simple message
     * @param message The message to display
     */
    public static void DisplayInfoMessage(string message)
    {
        EditorGUILayout.HelpBox(message, MessageType.Info);
    }
}
