using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Custom inspector to set up the arena
 * @class ArenaInspector
 */
[CustomEditor(typeof(Arena))]
public class ArenaInspector : Editor
{
    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        // Buffering info
        Arena instance = (Arena)target;

        // Rendering
        InspectorHelper.DisplayInfoMessage("Here you can set up the arena");
        InspectorHelper.DisplaySeparator("Physics");
        PhysicSection(instance);

        // Editor bug fix with custom window
        EditorUtility.SetDirty(instance);
    }

    /**
     * Arena physics settings section
     * @param instance The instance of the target (Arena)
     */
    private void PhysicSection(Arena instance)
    {
        EdgeCollider2D edgeCollider = instance.GetComponent<EdgeCollider2D>();

        EditorGUILayout.BeginVertical();

        float oldArenaRadius     = instance.arenaRadius;
        int   oldArenaResolution = instance.arenaResolution;

        instance.arenaCenter     = EditorGUILayout.Vector2Field("Arena center",     instance.arenaCenter);
        instance.arenaRadius     = EditorGUILayout.FloatField  ("Arena radius",     instance.arenaRadius);
        instance.arenaResolution = EditorGUILayout.IntField    ("Arena Resolution", instance.arenaResolution);

        // Setting up the edge collider on changes
        instance.transform.position = new Vector3(instance.arenaCenter.x, instance.arenaCenter.y);

        if(oldArenaRadius != instance.arenaRadius || oldArenaResolution != instance.arenaResolution)
        {
            OnArenaChange(edgeCollider, instance);
        }

        EditorGUILayout.EndHorizontal();
    }

    /**
     * The arena set up has changed
     * @param collider The edge collider of the arena
     * @param instance The instance (Arena)
     */
    private void OnArenaChange(EdgeCollider2D collider, Arena instance)
    {
        // Allocating the new array of points
        Vector2[] points = new Vector2[instance.arenaResolution + 1];

        // Computing each segment lenghts on the trigonometric circle
        float segmentLenght = 2 * Mathf.PI / instance.arenaResolution;

        for (int nSeg = 0; nSeg < instance.arenaResolution; ++nSeg)
        {
            Vector2 point;

            point.x = instance.arenaRadius * Mathf.Cos(segmentLenght * nSeg);
            point.y = instance.arenaRadius * Mathf.Sin(segmentLenght * nSeg);

            points[nSeg] = point;
        }

        // Adding the last points
        points[instance.arenaResolution] = points[0];

        // Assigning new points to the collider
        collider.points = points;
    }
}