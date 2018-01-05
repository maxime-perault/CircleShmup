using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Helper methods for views
 * @class PatternEditorHelperView
 */
public class PatternEditorHelperView
{
    /**
     * Draw a grid in the current GUI context from parameters
     * @param position The position (Rect) of the grid
     * @param offset   The current offset of the grid
     * @param drag     The drag offset
     * @param spacing  The spacing between lines
     * @param opacity  The opacity
     * @param colors   The color of the lines
     */
    public static void DrawGrid(Rect position, ref Vector2 offset, Vector2 drag, float spacing, float opacity, Color color)
    {
        int widthDivisions  = Mathf.CeilToInt(position.width / spacing);
        int heightDivisions = Mathf.CeilToInt(position.height / spacing);

        Handles.BeginGUI();
        Handles.color = new Color(color.r, color.g, color.b, opacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % spacing, offset.y % spacing, 0);

        for (int i = 0; i < widthDivisions; i++)
        {
            Handles.DrawLine(new Vector3(spacing * i, -spacing, 0) + newOffset, new Vector3(spacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivisions; j++)
        {
            Handles.DrawLine(new Vector3(-spacing, spacing * j, 0) + newOffset, new Vector3(position.width, spacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }
}