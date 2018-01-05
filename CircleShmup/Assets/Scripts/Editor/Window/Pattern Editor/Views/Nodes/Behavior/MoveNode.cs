using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * TODO
 * @class MoveNode
 */
[System.Serializable]
public class MoveNode : BaseNode
{
    /**
     * Delegated constructor, initializes a begin node
     * @param id The id of the node
     * @param x  The x coordinate of the node
     * @param y  The y coordinate of the node
     */
    public MoveNode(int id, float x, float y) : this(id, new Rect(x, y, 150.0f, 110.0f), "Move", NodeType.MoveNode)
    {
        // None
    }

    /**
     * Constructor, constructs the node serializable fields
     * @param id    The id of the node
     * @param rect  The rect (pos, width, height) of the node
     * @param title The title of the node
     * @param type  The true type of the node
     */
    public MoveNode(int id, Rect rect, string title, NodeType type) : base(id, rect, title, type)
    {
        nodeInPoints.Add (new ConnectionPointView(this, ConnectionPointViewType.In, 20.0f));
        nodeOutPoints.Add(new ConnectionPointView(this, ConnectionPointViewType.Out));

        // Add location field
        nodeInPoints.Add(new ConnectionPointView(this, ConnectionPointViewType.In, -5.0f));
    }

    /**
     * Draws the begin node
     */
    public override void Draw()
    {
        base.Draw();
        GUI.Label(new Rect(nodeRect.x + 50.0f, nodeRect.y + 16.0f, 100.0f, 20.0f), nodeTitle);

        // float x = 0.0f;
        // 
        // EditorGUI.LabelField(new Rect(nodeRect.x + 20.0f, nodeRect.y + 40.0f, 50.0f, 15.0f), "X");
        // EditorGUI.LabelField(new Rect(nodeRect.x + 20.0f, nodeRect.y + 60.0f, 50.0f, 15.0f), "Y");
        // 
        // x = EditorGUI.FloatField(new Rect(nodeRect.x + 40.0f, nodeRect.y + 40.0f, 80.0f, 15.0f), x);
        // x = EditorGUI.FloatField(new Rect(nodeRect.x + 40.0f, nodeRect.y + 60.0f, 80.0f, 15.0f), x);
    }
}