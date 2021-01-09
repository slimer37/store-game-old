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

        int lineLength = 15;
        Level level;
        if (level = FindObjectOfType<Level>())
        { lineLength = level.Capacity; }

        preview = EditorGUILayout.Toggle("Preview", preview);
        positions = QueuePositioning.GenerateQueue((Register)target, lineLength);
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
