using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Register)), CanEditMultipleObjects]
public class RegisterEditor : Editor
{
    private static bool preview = true;
    private static Vector3[] positions;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        preview = EditorGUILayout.Toggle("Preview", preview);
        int previewLineLength = serializedObject.FindProperty("lineLength").intValue;
        positions = QueuePositioning.GenerateQueue((Register)target, previewLineLength);
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Register reg, GizmoType type)
    {
        if (preview && positions.Length > 0)
        {
            foreach (var pos in positions)
            { Gizmos.DrawSphere(pos, 0.5f); }
        }
    }
}
