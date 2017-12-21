using UnityEngine;
using UnityEditor;

/**
 * Custom inspector for the player
 * @class PlayerControllerInspector
 */
[CustomEditor(typeof(PlayerController))]
public class EditorPlayerInspector : Editor
{
    private const int GUI_SPACE           = 1;
    private const int LAYOUT_BOOL_WIDTH   = 100;
    private const int LAYOUT_INT_WIDTH    = 200;
    private const int LAYOUT_STR_WIDTH    = 250;
    private const int LAYOUT_OBJ_WIDTH    = 300;
    private const int LAYOUT_VC3_WIDTH    = 350;
    private const int GUI_SPACE_SEPARATOR = 10;

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
        InspectorHelper.DisplayInfoMessage("You can modify all player settings from this custom editor window");
        InspectorHelper.DisplaySeparator("Rendering");
        RenderingSection(controller);

        // Gameplay
        InspectorHelper.DisplaySeparator("Gameplay");
        GameplaySection(controller);

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

        instance.slide  = EditorGUILayout.Toggle("Player Slide ", instance.slide);
        playerBody.drag = EditorGUILayout.Slider("Player Drag ", playerBody.drag, 0.0f, 20.0f);
        instance.speed  = EditorGUILayout.Vector2Field("Player Speed", instance.speed);

        EditorGUILayout.EndVertical();
    }
}
