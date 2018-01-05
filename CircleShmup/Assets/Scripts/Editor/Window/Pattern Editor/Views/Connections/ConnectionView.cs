using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * TODO
 * @class ConnectionPointView
 */
public class ConnectionView
{
    public ConnectionPointView inPointView;
    public ConnectionPointView outPointView;

    public ConnectionView(ConnectionPointView inPoint, ConnectionPointView outPoint)
    {
        inPointView  = inPoint;
        outPointView = outPoint;
    }

    public void Draw()
    {
        Handles.DrawBezier
        (
            inPointView.pointRect.center,
            outPointView.pointRect.center,
            inPointView.pointRect.center + Vector2.left * 50f,
            outPointView.pointRect.center - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );

        if (Handles.Button((inPointView.pointRect.center + outPointView.pointRect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            PatternEditorNodeView.OnClickRemoveConnection(this);
        }
    }
}