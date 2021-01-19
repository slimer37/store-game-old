using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
    private static float anchorHeight;
    private static float triggerHeight;
    private static Bounds colBounds;
    private static Vector3 containerPos;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var selected = (Container)target;
        anchorHeight = serializedObject.FindProperty("scaleAnchor").floatValue;
        triggerHeight = serializedObject.FindProperty("triggerHeight").floatValue;

        Collider trigger = null;
        foreach (var col in selected.GetComponentsInChildren<Collider>())
        {
            if (col.isTrigger)
            {
                trigger = col;
                colBounds = col.bounds;
            }
        }
        if (triggerHeight > trigger.bounds.max.y - selected.transform.position.y)
        { EditorGUILayout.HelpBox("Trigger height is above collider bounds.", MessageType.Warning); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Container container, GizmoType type)
    {
        var offset = new Vector3(colBounds.size.x * 0.5f, 0, colBounds.size.z * 0.5f);
        var secondOffset = offset - Vector3.forward * colBounds.size.z;

        containerPos = container.transform.position;
        DrawThinCube(Color.yellow, triggerHeight);
        DrawThinCube(Color.red, anchorHeight);

        void DrawThinCube(Color color, float height)
        {
            Gizmos.color = color;
            var pos = new Vector3(colBounds.center.x, containerPos.y + height, colBounds.center.z);
            Gizmos.DrawWireCube(pos, new Vector3(colBounds.size.x, 0.01f, colBounds.size.z));
            Gizmos.DrawLine(pos + offset, pos - offset);
            Gizmos.DrawLine(pos + secondOffset, pos - secondOffset);
        }
    }

    void OnSceneGUI()
    {
        Handles.Label(new Vector3(colBounds.center.x, containerPos.y + triggerHeight, colBounds.center.z), "Trigger");
        Handles.Label(new Vector3(colBounds.center.x, containerPos.y + anchorHeight, colBounds.center.z), "Anchor");
    }
}
