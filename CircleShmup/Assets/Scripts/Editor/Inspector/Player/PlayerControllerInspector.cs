using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for the player
 * @class PlayerControllerInspector
 */
[CustomEditor(typeof(PlayerController))]
public class PlayerInspector : Editor
{
    private Rigidbody2D playerBody;

    /**
     * Called to draw the custom inspector
     */
    public override void OnInspectorGUI()
    {
        // Buffering info
        PlayerController controller = (PlayerController)target;
        playerBody = controller.GetComponent<Rigidbody2D>();

        // Rendering
        InspectorHelper.DisplaySeparator("Rendering");
        RenderingSection(controller);

        // Gameplay
        InspectorHelper.DisplaySeparator("Gameplay");
        GameplaySection(controller);

        DrawDefaultInspector();

        // Editor bug fix with custom window
        EditorUtility.SetDirty(controller);
    }

    /**
     * Custom inspector for player redering properties
     * @param instance The instance of the target (PlayerController)
     */
    private void RenderingSection(PlayerController instance)
    {
        InspectorHelper.DisplayInfoMessage("No features yet. Coming soon.");
    }

    /**
     * Custom inspector for player gameplay properties
     * @param instance The instance of the target (PlayerController)
     */
    private void GameplaySection(PlayerController instance)
    {
        EditorGUILayout.BeginVertical();

        instance.hitPoint          = EditorGUILayout.IntField("Player hit points",    instance.hitPoint);
        instance.damageOnCollision = EditorGUILayout.IntField("Damages on collision", instance.damageOnCollision);

        instance.slide             = EditorGUILayout.Toggle("Player Slide ",      instance.slide);
        playerBody.drag            = EditorGUILayout.Slider("Player Drag ",       playerBody.drag, 0.0f, 20.0f);
        instance.speed             = EditorGUILayout.Vector2Field("Player Speed", instance.speed);

        EditorGUILayout.EndVertical();
    }
}
