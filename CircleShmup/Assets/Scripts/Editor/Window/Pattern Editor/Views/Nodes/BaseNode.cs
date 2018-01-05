using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

/**
 * Base class for all nodes
 */
[System.Serializable]
public class BaseNode
{
    public enum NodeType
    {
        BaseNode,
        BeginNode,
        EndNode,

        Vector2,

        MoveNode
    }

    [SerializeField] public int      nodeID;
    [SerializeField] public Rect     nodeRect;
    [SerializeField] public string   nodeTitle;
    [SerializeField] public NodeType nodeType;

    // TODO : Serialize
    public List<ConnectionPointView> nodeInPoints;
    public List<ConnectionPointView> nodeOutPoints;

    [System.NonSerialized] public bool isDragged;
    [System.NonSerialized] public bool isSelected;

    [System.NonSerialized] public GUIStyle style;
    [System.NonSerialized] public GUIStyle defaultNodeStyle;
    [System.NonSerialized] public GUIStyle selectedNodeStyle;
        
    /**
     * Constructor, constructs the node serializable fields
     * @param id    The id of the node
     * @param rect  The rect (pos, width, height) of the node
     * @param title The title of the node
     * @param type  The true type of the node
     */
    public BaseNode(int id, Rect rect, string title, NodeType type)
    {
        nodeID    = id;
        nodeRect  = rect;
        nodeTitle = title;
        nodeType  = type;

        isDragged  = false;
        isSelected = false;

        style             = PatternEditorSkinModel.styleNode;
        defaultNodeStyle  = PatternEditorSkinModel.styleNode;
        selectedNodeStyle = PatternEditorSkinModel.styleSelectedNode;

        nodeInPoints  = new List<ConnectionPointView>();
        nodeOutPoints = new List<ConnectionPointView>();
    }

    /**
     * Process the event
     * Check if there is an event related to this node
     * @param e The event to process
     */
    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
            {
                if (e.button == 0)
                {
                    if (nodeRect.Contains(e.mousePosition))
                    {
                        isDragged   = true;
                        isSelected  = true;
                        GUI.changed = true;
                        
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected  = false;

                        style = defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && nodeRect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;
            }

            case EventType.MouseDrag:
            {
                if (e.button == 0 && isDragged)
                {
                    DragNode(e.delta); e.Use();
                    return true;
                }

                break;
            }

            case EventType.MouseUp:
            {
                isDragged = false;
                break;
            } 
        }

        return false;
    }

    /**
     * Shows a small context menu to 
     * remove or reset the current node
     */
    public virtual void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Reset node"), false, OnClickResetNode);
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    /**
     * Resets the node to defaults values
     */
    public virtual void OnClickResetNode()
    {
        // None
    }

    /**
     * Removes the node from the graph
     */
    public virtual void OnClickRemoveNode()
    {
        // TODO
    }

    /**
     * Moves the node from the given delta
     * @param delta The delta position to add
     */
    public void DragNode(Vector2 delta)
    {
        nodeRect.position += delta;
    }

    /**
     * Renders the node box and connections
     */
    public virtual void Draw()
    {
        GUI.Box(nodeRect, "", style);

        int inCount  = nodeInPoints.Count;
        int outCount = nodeOutPoints.Count;

        for(int nInPoint = 0; nInPoint < inCount; ++nInPoint)
        {
            nodeInPoints[nInPoint].Draw();
        }

        for (int nOutPoint = 0; nOutPoint < outCount; ++nOutPoint)
        {
            nodeOutPoints[nOutPoint].Draw();
        }
    } 
}