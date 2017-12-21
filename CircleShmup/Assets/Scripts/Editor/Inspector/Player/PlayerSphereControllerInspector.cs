using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for the sphere controller
 * @class PlayerSphereControllerInspector
 */
[CustomEditor(typeof(PlayerSphereController))]
public class PlayerSphereControllerInspector : Editor
{
    private const int GUI_SPACE           = 1;
    private const int LAYOUT_BOOL_WIDTH   = 100;
    private const int LAYOUT_INT_WIDTH    = 200;
    private const int LAYOUT_STR_WIDTH    = 250;
    private const int LAYOUT_OBJ_WIDTH    = 350;
    private const int LAYOUT_VC3_WIDTH    = 350;
    private const int GUI_SPACE_SEPARATOR = 10;

    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        // Getting target
        PlayerSphereController controller = (PlayerSphereController)target;

        InspectorHelper.DisplaySeparator("Weapon");
        WeaponSection(controller);

        InspectorHelper.DisplaySeparator("Debug");
        DebugSection(controller);

        EditorUtility.SetDirty(controller);
    }

    /**
     * Displays weapon controller properties
     * https://docs.unity3d.com/ScriptReference/EditorGUILayout.ObjectField.html
     */
    private void WeaponSection(PlayerSphereController instance)
    {
        EditorGUILayout.BeginVertical();

        instance.spherePrefab = EditorGUILayout.ObjectField("Shpere prefab", instance.spherePrefab, 
            typeof(GameObject), true) as GameObject;

        InspectorHelper.DisplayInfoMessage("This is the cooldown between two rotation direction inversion");
        instance.reverseDelay = EditorGUILayout.FloatField("Reverse delay", instance.reverseDelay);

        InspectorHelper.DisplayInfoMessage("This is the cooldown between two sphere addings");
        instance.sphereDelay       = EditorGUILayout.FloatField("Sphere delay", instance.sphereDelay);
        instance.radius            = EditorGUILayout.FloatField("Current radius", instance.radius);
        instance.minRadius         = EditorGUILayout.FloatField("Minimal radius", instance.minRadius);
        instance.maxRadius         = EditorGUILayout.FloatField("Maximal radius", instance.maxRadius);
        instance.radiusCrunchSpeed = EditorGUILayout.FloatField("Radius crunch speed", instance.radiusCrunchSpeed);
        instance.radiusGrowSpeed   = EditorGUILayout.FloatField("Radius grow speed", instance.radiusGrowSpeed);
        instance.rotationSpeed     = EditorGUILayout.FloatField("Base rotation speed", instance.rotationSpeed);
        instance.startSphereCount  = EditorGUILayout.IntField("Start sphere count", instance.startSphereCount);

        EditorGUILayout.EndVertical();
    }

    /**
     * Displays read only info
     */
    private void DebugSection(PlayerSphereController instance)
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.IntField("Current Sphere count", instance.spheres.Count);
        EditorGUILayout.Toggle("Can reverse ?", instance.canReverse);
        EditorGUILayout.Toggle("Can add ?", instance.canAddSphere);

        EditorGUILayout.EndVertical();
    }
}
