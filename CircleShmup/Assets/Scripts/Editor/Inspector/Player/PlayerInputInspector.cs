using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for input settings
 * @class PlayerInputController
 */
[CustomEditor(typeof(PlayerInputController))]
public class PlayerInputInspector : Editor
{
    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        // Buffering info
        PlayerInputController controller = (PlayerInputController)target;

        // Rendering
        InspectorHelper.DisplayInfoMessage("You can modify all player settings from this custom editor window");
        InspectorHelper.DisplaySeparator("Player input settings");
        InputSection(controller);

        // Editor bug fix with custom window
        EditorUtility.SetDirty(controller);
    }

    /**
     * Custom inspector for player input settings
     * @param instance The instance of the target (PlayerInputController)
     */
    private void InputSection(PlayerInputController instance)
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(300.0f));

        instance.canAddSphere       = EditorGUILayout.Toggle("Allow add sphere ?",       instance.canAddSphere);
        instance.canRemoveSphere    = EditorGUILayout.Toggle("Allow remove sphere ?",    instance.canRemoveSphere);
        instance.canIncreaseRadius  = EditorGUILayout.Toggle("Allow increase radius ?",  instance.canIncreaseRadius);
        instance.canReverseRotation = EditorGUILayout.Toggle("Allow reverse rotation ?", instance.canReverseRotation);

        EditorGUILayout.Space();

        instance.verticalInput   = EditorGUILayout.TextField("Vertical input",   instance.verticalInput);
        instance.horizontalInput = EditorGUILayout.TextField("Horizontal input", instance.horizontalInput);

        instance.addSphereInput       = EditorGUILayout.TextField("Add sphere input",       instance.addSphereInput);
        instance.removeSphereInput    = EditorGUILayout.TextField("Remove sphere input",    instance.removeSphereInput);
        instance.increaseRadiusInput  = EditorGUILayout.TextField("Increase radius input",  instance.increaseRadiusInput);
        instance.reverseRotationInput = EditorGUILayout.TextField("Reverse rotation input", instance.reverseRotationInput);

        EditorGUILayout.EndVertical();
    }
}