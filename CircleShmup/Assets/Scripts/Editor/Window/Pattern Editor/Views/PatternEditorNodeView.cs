using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

/**
 * Displays all nodes
 * @class PatternEditorNodeView
 */
public class PatternEditorNodeView : EditorWindow
{
    private Vector2 drag;
    private Vector2 offset;

    private ConnectionPointView selectedInPoint;
    private ConnectionPointView selectedOutPoint;

    private List<BaseNode>   nodes           = new List<BaseNode>();
    private List<ConnectionView> connections = new List<ConnectionView>();

    private static PatternEditorNodeView instance;

    /**
     * TODO
     */
    public static void OpenPatternEditorNodeView()
    {
        PatternEditorNodeView window = GetWindow<PatternEditorNodeView>();

        // Setting up the window
        window.titleContent = new GUIContent("Node view");
        window.minSize = new Vector2(1280.0f, 720.0f);

        float x = (Screen.width - window.minSize.x)  / 2.0f;
        float y = (Screen.height - window.minSize.y) / 2.0f;

        // Resizing and centering the window
        window.position = new Rect(x, y, window.minSize.x, window.minSize.y);
    }

    /**
     * TODO
     */
    private void OnEnable()
    {
        instance = this;
        PatternEditorSkinModel.LoadSkins();
    }

    /**
     * TODO
     */
    private void OnGUI()
    {
        PatternEditorHelperView.DrawGrid(position, ref offset, drag, 20.0f,  0.2f, Color.gray);
        PatternEditorHelperView.DrawGrid(position, ref offset, drag, 100.0f, 0.4f, Color.gray);

        // Draws all elements
        DrawNodes();
        DrawConnections();
        DrawConnectionLine(Event.current);

        // Process all events
        ProcessNodeEvents(Event.current);
        ProcessEvents    (Event.current);
        
        // If there is something that changed
        // We have to repaint the entire window
        if (GUI.changed)
        {
            Repaint();
        }
    }

    /**
     * TODO
     */
    private void DrawNodes()
    {
        if (nodes != null)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Draw();
            }
        }
    }

    /**
     * TODO
     */
    private void DrawConnections()
    {
        if (connections != null)
        {
            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }
        }
    }

    /**
     * TODO
     */
    private void DrawConnectionLine(Event e)
    {
        if (selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.pointRect.center,
                e.mousePosition,
                selectedInPoint.pointRect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (selectedOutPoint != null && selectedInPoint == null)
        {
            Handles.DrawBezier(
                selectedOutPoint.pointRect.center,
                e.mousePosition,
                selectedOutPoint.pointRect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    /**
     * TODO
     */
    private void ProcessNodeEvents(Event e)
    {
        int nodeCount = nodes.Count;
        for (int nNode = nodeCount - 1; nNode >= 0; --nNode)
        {
            bool guiChanged = nodes[nNode].ProcessEvents(e);

            if (guiChanged)
            {
                GUI.changed = true;
            }
        }
    }

    /**
     * TODO
     */
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;
        Debug.Log("ProcessEvents");
        if (e.type == EventType.mouseDown && e.button == 0)
        {
            ClearConnectionSelection();
        }

        if (e.type == EventType.mouseDown && e.button == 1)
        {
            ProcessContextMenu(e.mousePosition);
        }
        
        if(e.type == EventType.MouseDrag && e.button == 0)
        {
            OnDrag(e.delta);
        }
    }

    /**
     * TODO
     */
    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        int nodeCount = nodes.Count;
        for (int nNode = 0; nNode < nodeCount; nNode++)
        {
            nodes[nNode].DragNode(delta);
        }

        GUI.changed = true;
    }

    /**
     * TODO
     */
    private void ProcessContextMenu(Vector2 mousePosition)
    {
        // TODO
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Begin Node"), false, () => OnClickAddBeginNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add End Node"), false, () => OnClickAddEndNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Vector 2 Node"), false, () => OnClickAddVector2Node(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Move Node"), false, () => OnClickAddMoveNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    /**
     * TODO
     */
    private void OnClickAddBeginNode(Vector2 mousePosition)
    {
        nodes.Add(new BeginNode(0, mousePosition.x, mousePosition.y));
    }

    /**
     * TODO
     */
    private void OnClickAddEndNode(Vector2 mousePosition)
    {
        nodes.Add(new EndNode(0, mousePosition.x, mousePosition.y));
    }

    /**
     * TODO
     */
    private void OnClickAddVector2Node(Vector2 mousePosition)
    {
        nodes.Add(new Vector2Node(0, mousePosition.x, mousePosition.y));
    }

    /**
     * TODO
     */
    private void OnClickAddMoveNode(Vector2 mousePosition)
    {
        nodes.Add(new MoveNode(0, mousePosition.x, mousePosition.y));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint  = null;
        selectedOutPoint = null;
    }





    public static void OnClickInPoint(ConnectionPointView inPoint)
    {
        instance.selectedInPoint = inPoint;

        if (instance.selectedOutPoint != null)
        {
            if (instance.selectedOutPoint.pointNode != instance.selectedInPoint.pointNode)
            {
                instance.CreateConnection();
                instance.ClearConnectionSelection();
            }
            else
            {
                instance.ClearConnectionSelection();
            }
        }
    }

    public static void OnClickOutPoint(ConnectionPointView outPoint)
    {
        instance.selectedOutPoint = outPoint;

        if (instance.selectedInPoint != null)
        {
            if (instance.selectedOutPoint.pointNode != instance.selectedInPoint.pointNode)
            {
                instance.CreateConnection();
                instance.ClearConnectionSelection();
            }
            else
            {
                instance.ClearConnectionSelection();
            }
        }
    }

    public static void OnClickRemoveConnection(ConnectionView connection)
    {
        instance.connections.Remove(connection);
    }

    public static void OnClickRemoveNode(BaseNode node)
    {
        //if (instance.connections != null)
        //{
        //    List<Connection> connectionsToRemove = new List<Connection>();
        //
        //    for (int i = 0; i < connections.Count; i++)
        //    {
        //        if (connections[i].inPoint == node.inPoint || connections[i].outPoint == node.outPoint)
        //        {
        //            connectionsToRemove.Add(connections[i]);
        //        }
        //    }
        //
        //    for (int i = 0; i < connectionsToRemove.Count; i++)
        //    {
        //        connections.Remove(connectionsToRemove[i]);
        //    }
        //
        //    connectionsToRemove = null;
        //}

        instance.nodes.Remove(node);
    }

    private void CreateConnection()
    {
        connections.Add(new ConnectionView(selectedInPoint, selectedOutPoint));
    }
}