using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tool), true)]
public class ToolEditor : Editor
{
    private static Vector3 selectedHoldPosition = Vector3.one;
    private static Quaternion selectedHoldRotation = Quaternion.identity;
    private static Mesh mesh;
    private static bool preview;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!((Tool)target).GetComponent<MeshFilter>())
        {
            EditorGUILayout.HelpBox("Cannot preview without a MeshFilter on this object.", MessageType.Info);
            return;
        }

        mesh = ((Tool)target).GetComponent<MeshFilter>().sharedMesh;

        var rot = Quaternion.Euler(serializedObject.FindProperty("holdRotation").vector3Value);
        var pos = serializedObject.FindProperty("holdPosition").vector3Value;
        var prev = GUILayout.Toggle(preview, "Preview", "Button");

        // Update gizmos when pos/rot values or preview bool updates.
        if (selectedHoldRotation != (selectedHoldRotation = rot)
            || selectedHoldPosition != (selectedHoldPosition = pos)
            || preview != (preview = prev))
        { EditorUtility.SetDirty(target); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Container container, GizmoType type)
    {
        if (preview)
        {
            Gizmos.color = Color.white;
            Transform parent = Camera.main.transform.parent;
            Gizmos.DrawMesh(mesh, parent.TransformPoint(selectedHoldPosition), parent.rotation * selectedHoldRotation);
        }
    }
}
