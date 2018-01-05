using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * TODO
 */
public enum ConnectionPointViewType
{
    In,
    Out
}

/**
 * TODO
 * @class ConnectionPointView
 */
public class ConnectionPointView
{
    public Rect     pointRect;
    public float    pointOffset;
    public BaseNode pointNode;
    public GUIStyle pointStyle;

    public ConnectionPointViewType pointType;

    /**
     * TODO
     */
    public ConnectionPointView(BaseNode node, ConnectionPointViewType type) : this(node, type, 0.0f)
    {
        // None
    }

    /**
     * TODO
     */
    public ConnectionPointView(BaseNode node, ConnectionPointViewType type, float offset)
    {
        pointNode   = node;
        pointType   = type;
        pointOffset = offset;
        pointRect   = new Rect(0.0f, 0.0f, 10.0f, 20.0f);

        if (pointType == ConnectionPointViewType.In) pointStyle = PatternEditorSkinModel.styleInPoint;
        if (pointType == ConnectionPointViewType.Out) pointStyle = PatternEditorSkinModel.styleOutPoint;
    }

    /**
     * TODO
     */
    public void Draw()
    {
        pointRect.y  = pointNode.nodeRect.y + (pointNode.nodeRect.height * 0.5f) - pointRect.height * 0.5f;
        pointRect.y += pointOffset;

        switch (pointType)
        {
            case ConnectionPointViewType.In:
                pointRect.x = pointNode.nodeRect.x - pointRect.width + 7.0f;
                break;

            case ConnectionPointViewType.Out:
                pointRect.x = pointNode.nodeRect.x + pointNode.nodeRect.width - 7.0f;
                break;
        }

        if (GUI.Button(pointRect, "", pointStyle))
        {
            if(pointType == ConnectionPointViewType.In)
                PatternEditorNodeView.OnClickInPoint(this);
            else
                PatternEditorNodeView.OnClickOutPoint(this);
        }
    }
}
