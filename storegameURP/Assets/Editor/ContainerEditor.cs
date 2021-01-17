using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Container))]
public class ContainerEditor : Editor
{
    private static float anchorHeight;
    private static float triggerHeight;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var selected = (Container)target;
        anchorHeight = serializedObject.FindProperty("scaleAnchor").floatValue;
        triggerHeight = serializedObject.FindProperty("triggerHeight").floatValue;
        if (triggerHeight > selected.GetComponent<Collider>().bounds.max.y - selected.transform.position.y)
        { EditorGUILayout.HelpBox("Trigger height is above collider bounds.", MessageType.Warning); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Container container, GizmoType type)
    {
        var offset = new Vector3(0.5f, 0, 0.5f);
        var secondOffset = new Vector3(0.5f, 0, -0.5f);

        Gizmos.color = Color.yellow;
        var triggerPos = container.transform.position + Vector3.up * triggerHeight;
        Gizmos.DrawWireCube(triggerPos, new Vector3(1, 0.01f, 1));
        Gizmos.DrawLine(triggerPos + offset, triggerPos - offset);
        Gizmos.DrawLine(triggerPos + secondOffset, triggerPos - secondOffset);

        Gizmos.color = Color.red;
        var anchorPos = container.transform.position + Vector3.up * anchorHeight;
        Gizmos.DrawWireCube(anchorPos, new Vector3(1, 0.01f, 1));
        Gizmos.DrawLine(anchorPos + offset, anchorPos - offset);
        Gizmos.DrawLine(anchorPos + secondOffset, anchorPos - secondOffset);
    }
}
