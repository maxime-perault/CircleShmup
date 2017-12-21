using UnityEngine;
using UnityEditor;

/**
 * Custom player setting editor window
 * @class PlayerControllerWindow
 */
public class PlayerControllerWindow : EditorWindow
{
    static private Vector2 scrollPos;
    static private Vector2 windowSize;
    static private PlayerControllerWindow window;

    /**
     * Initializes the window
     */
    [MenuItem("Shmup/Player Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        window = (PlayerControllerWindow)EditorWindow.GetWindow(typeof(PlayerControllerWindow));
        window.Show();

        // Configuring the window
        window.titleContent.text = "Player settings";
        window.maxSize = new Vector2(370, 550);
        window.minSize = new Vector2(200, 300);
    }

    /**
     * Displays the inspector in the custom window
     */
    void OnGUI()
    {
        PlayerController playerController       = UnityEngine.GameObject.FindGameObjectWithTag("Player"). GetComponent<PlayerController>();
        PlayerSphereController sphereController = UnityEngine.GameObject.FindGameObjectWithTag("Spheres").GetComponent<PlayerSphereController>();

        Editor editorPlayer = Editor.CreateEditor(playerController);
        Editor editorWeapon = Editor.CreateEditor(sphereController);

        // Adding the scroll bar
        EditorGUILayout.BeginHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        editorPlayer.OnInspectorGUI();
        editorWeapon.OnInspectorGUI();

        // End of scroll bar
        EditorGUILayout.EndScrollView();
    }

    /**
     * Makes sure that the inspector is always updated
     */ 
    void Update()
    {
        if (EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            Repaint();
        }
    }
}