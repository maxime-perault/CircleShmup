using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * This manager can display messages to the player
 * @class MessageManager
 */
public class MessageManager : MonoBehaviour
{
    public GameObject parent;
    public GameObject messagePrefab;

    private static MessageManager instance;

    /**
     * Called once at loading time
     */
    void Awake()
    {
        instance = this;
    }

    /**
     * Display a simple message at the middle of the screen
     * @param message The message to display
     * @param duration The duration of the message
     */
    public static GameObject Message(string message, float duration)
    {
        GameObject go = Instantiate(instance.messagePrefab, instance.parent.transform);
        go.transform.position -= new Vector3(0.0f, 32.0f * instance.parent.transform.lossyScale.y, 0.0f);

        Text text = go.GetComponent<Text>();
        text.text = message;

        Destroy(go, duration);
        return go;
    }
}
