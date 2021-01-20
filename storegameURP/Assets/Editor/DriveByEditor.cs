using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DriveBy))]
public class DriveByEditor : Editor
{
    private static SerializedProperty nodes;
    private static bool editing;
    private static bool localHandles;

    void Awake() => nodes = serializedObject.FindProperty("nodes");

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();

        if (editing != GUILayout.Toggle(editing, "Edit Nodes", "Button"))
        {
            EditorUtility.SetDirty(target);
            editing = !editing;
        }

        if (editing)
        {
            if (localHandles != GUILayout.Toggle(localHandles, "Align Handles to Path"))
            {
                localHandles = !localHandles;
                EditorUtility.SetDirty(target);
            }
        }

        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
        nodes = serializedObject.FindProperty("nodes");
    }

    void OnSceneGUI()
    {
        if (!editing) return;

        Vector3[] points = new Vector3[nodes.arraySize];
        for (int i = 0; i < nodes.arraySize; i++)
        { points[i] = nodes.GetArrayElementAtIndex(i).vector3Value; }

        Handles.color = Color.yellow;
        Handles.DrawPolyLine(points);

        for (int i = 0; i < nodes.arraySize; i++)
        {
            Quaternion rot = localHandles && i < nodes.arraySize - 1 ? Quaternion.LookRotation(points[i + 1] - points[i]) : Quaternion.identity;
            nodes.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(points[i], rot);
            Handles.Label(points[i], "Element " + i);
        }

        serializedObject.ApplyModifiedProperties();
    }

    [DrawGizmo(GizmoType.Active | GizmoType.Active)]
    static void DrawGizmos(DriveBy driveBy, GizmoType type)
    {
        if (!editing) return;

        for (int i = 0; i < nodes.arraySize; i++)
        {
            Gizmos.color = new Color(1, 0.5f, 0);
            if (i == 0)
            { Gizmos.color = Color.green; }
            else if (i == nodes.arraySize - 1)
            { Gizmos.color = Color.red; }

            Gizmos.DrawSphere(nodes.GetArrayElementAtIndex(i).vector3Value, 0.5f);
        }
    }
}
