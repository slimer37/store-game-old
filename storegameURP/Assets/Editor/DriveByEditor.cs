using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DriveBy)), CanEditMultipleObjects]
public class DriveByEditor : Editor
{
    private static SerializedProperty nodes;
    private static bool editing;
    private static bool localHandles;

    private Vector3 incrementAmount;

    void Awake() => nodes = serializedObject.FindProperty("nodes");

    public override void OnInspectorGUI()
    {
        if (targets.Length > 1)
        {
            EditorGUILayout.HelpBox("Can only edit nodes of one vehicle at a time.", MessageType.Warning);
            return;
        }

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();
        if (editing != GUILayout.Toggle(editing, "Edit Nodes", "Button", GUILayout.Width(Screen.width / 2)))
        {
            EditorUtility.SetDirty(target);
            editing = !editing;
        }
        GUILayout.FlexibleSpace();

        if (editing)
        {
            if (localHandles != GUILayout.Toggle(localHandles, "Align Handles to Path"))
            {
                localHandles = !localHandles;
                EditorUtility.SetDirty(target);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Shift All") && incrementAmount.magnitude != 0)
            {
                for (int i = 0; i < nodes.arraySize; i++)
                { nodes.GetArrayElementAtIndex(i).vector3Value += incrementAmount; }

                serializedObject.ApplyModifiedProperties();
            }

            incrementAmount = EditorGUILayout.Vector3Field("", incrementAmount);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Reverse Nodes"))
            {
                var points = new Vector3[nodes.arraySize];
                for (int i = 0; i < nodes.arraySize; i++)
                { points[i] = nodes.GetArrayElementAtIndex(i).vector3Value; }

                for (int i = 0; i < nodes.arraySize; i++)
                { nodes.GetArrayElementAtIndex(nodes.arraySize - 1 - i).vector3Value = points[i]; }

                serializedObject.ApplyModifiedProperties();
            }
        }
        else
        { EditorGUILayout.EndHorizontal(); }

        GUILayout.Space(10);

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
            Quaternion rot;
            if (localHandles)
            { rot = Quaternion.LookRotation(i < nodes.arraySize - 1 ? (points[i + 1] - points[i]) : (points[i] - points[i - 1])); }
            else
            { rot = Quaternion.identity; }
            nodes.GetArrayElementAtIndex(i).vector3Value = Handles.PositionHandle(points[i], rot);
            Handles.Label(points[i], "Element " + i);
        }

        nodes.serializedObject.ApplyModifiedProperties();
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
