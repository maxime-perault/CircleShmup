using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Last node, end point of the pattern
 * @class EndNode
 */
[System.Serializable]
public class EndNode : BaseNode
{
    /**
     * Delegated constructor, initializes a begin node
     * @param id The id of the node
     * @param x  The x coordinate of the node
     * @param y  The y coordinate of the node
     */
    public EndNode(int id, float x, float y) : this(id, new Rect(x, y, 100.0f, 50.0f), "End", NodeType.EndNode)
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
    public EndNode(int id, Rect rect, string title, NodeType type) : base(id, rect, title, type)
    {
        nodeInPoints.Add(new ConnectionPointView(this, ConnectionPointViewType.In));
    }

    /**
     * Draws the begin node
     */
    public override void Draw()
    {
        base.Draw();
        GUI.Label(new Rect(nodeRect.x + 33.0f, nodeRect.y + 16.0f, 50.0f, 20.0f), nodeTitle);
    }
}