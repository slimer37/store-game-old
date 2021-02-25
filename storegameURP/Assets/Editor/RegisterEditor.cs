using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Register)), CanEditMultipleObjects]
public class RegisterEditor : Editor
{
    private static bool preview = true;
    private static Vector3[] positions = new Vector3[] { };
    private static int lineLength;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        lineLength = 10;
        Level level;
        if (level = FindObjectOfType<Level>())
        { lineLength = level.Capacity; }

        if (preview = EditorGUILayout.BeginFoldoutHeaderGroup(preview, "Preview"))
        {
            Register reg = (Register)target;

            if (EditorApplication.isPlaying)
            {
                positions = reg.QueuePositions;
                EditorGUILayout.HelpBox("Currently displaying the generated queue positions.", MessageType.None);
            }
            else
            {
                if (GUILayout.Button("Cycle Positions") || positions.Length == 0)
                {
                    positions = QueuePositioning.GenerateQueue(reg, lineLength);
                    SceneView.RepaintAll();
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Register reg, GizmoType type)
    {
        if (!preview) return;

        if (positions.Length > 0)
        {
            foreach (var pos in positions)
            { Gizmos.DrawSphere(pos, 0.5f); }
        }
    }
}
