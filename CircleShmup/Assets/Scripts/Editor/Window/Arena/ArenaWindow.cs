using UnityEngine;
using UnityEditor;

/**
 * Custom arena setting editor window
 * @class ArenaWindow
 */
public class ArenaWindow : EditorWindow
{
    static private Vector2 scrollPos;

    /**
     * Initializes the window
     */
    [MenuItem("Shmup/Arena Settings")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ArenaWindow window = (ArenaWindow)EditorWindow.GetWindow(typeof(ArenaWindow));
        window.Show();

        // Configuring the window
        window.titleContent.text = "Arena settings";
        window.maxSize = new Vector2(265, 250);
        window.minSize = new Vector2(200, 150);
    }

    /**
     * Displays the inspector in the custom window
     */
    void OnGUI()
    {
        // Find the arena
        Arena arena = UnityEngine.GameObject.FindGameObjectWithTag("Arena").GetComponent<Arena>();

        // And make it active
        Selection.activeGameObject = arena.gameObject;

        // Finally draw the editor
        Editor arenaEditor = Editor.CreateEditor(arena);
        arenaEditor.OnInspectorGUI();
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