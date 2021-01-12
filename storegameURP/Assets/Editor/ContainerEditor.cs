using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Container))]
public class ToolEditor : Editor
{
    private static Vector3 selectedHoldPosition;
    private static Quaternion selectedHoldRotation;
    private static Mesh mesh;
    private static bool preview;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        mesh = ((Container)target).GetComponent<MeshFilter>().sharedMesh;

        if (selectedHoldRotation != (selectedHoldRotation = Quaternion.Euler(serializedObject.FindProperty("holdRotation").vector3Value))
            || selectedHoldPosition != (selectedHoldPosition = serializedObject.FindProperty("holdPosition").vector3Value)
            || preview != (preview = EditorGUILayout.Toggle("Preview", preview, "Button")))
        { EditorUtility.SetDirty(target); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(Container container, GizmoType type)
    {
        if (preview)
        {
            Gizmos.color = Color.white;
            Transform parent = Camera.main.transform.parent;
            Quaternion adjustedRotation = Quaternion.Inverse(parent.rotation * selectedHoldRotation);
            Gizmos.DrawMesh(mesh, parent.TransformPoint(selectedHoldPosition), adjustedRotation);
        }
    }
}
