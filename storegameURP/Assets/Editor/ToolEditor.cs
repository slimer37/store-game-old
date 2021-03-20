using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tool), true)]
public class ToolEditor : Editor
{
    private static Vector3 holdPos = Vector3.one;
    private static Quaternion holdRot = Quaternion.identity;
    private static Mesh mesh;
    private static bool preview;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        base.OnInspectorGUI();

        if (!((Tool)target).GetComponent<MeshFilter>())
        {
            EditorGUILayout.HelpBox("Cannot preview without a MeshFilter on this object.", MessageType.Info);
            return;
        }

        mesh = ((Tool)target).GetComponent<MeshFilter>().sharedMesh;

        holdRot = Quaternion.Euler(serializedObject.FindProperty("holdRotation").vector3Value);
        holdPos = serializedObject.FindProperty("holdPosition").vector3Value;
        preview = GUILayout.Toggle(preview, "Preview", "Button");

        // Update gizmos when pos/rot values or preview bool updates.
        if (EditorGUI.EndChangeCheck())
        { EditorUtility.SetDirty(target); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Container container, GizmoType type)
    {
        if (preview)
        {
            Gizmos.color = Color.white;
            Transform parent = Camera.main.transform.parent;
            Gizmos.DrawMesh(mesh, parent.TransformPoint(holdPos), parent.rotation * holdRot);
        }
    }
}
