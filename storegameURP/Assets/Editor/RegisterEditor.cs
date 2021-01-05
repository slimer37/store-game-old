using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Register)), CanEditMultipleObjects]
public class RegisterEditor : Editor
{
    private SerializedObject register;
    private Transform registerTransform;
    private bool foldout;

    private int visualizedLineLength;
    private float startOffset;
    private float lineOffset;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, "Visualize"))
        {
            registerTransform = ((Register)target).transform;
            register = serializedObject;
            startOffset = register.FindProperty("lineStartOffset").floatValue;
            lineOffset = register.FindProperty("lineCustomerOffset").floatValue;

            visualizedLineLength = EditorGUILayout.IntSlider("Visualized Line Length", visualizedLineLength, 0, 10);

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }

    void OnSceneGUI()
    {
        if (foldout)
        {
            Vector3 start = registerTransform.position - registerTransform.forward * startOffset;
            for (int i = 0; i < visualizedLineLength; i++)
            {
                Vector3 pos = start - registerTransform.forward * i * lineOffset;
                Vector3 camForward = SceneView.currentDrawingSceneView.camera.transform.forward;

                if (i > 0)
                {
                    Vector3 p1 = pos + registerTransform.forward * 0.5f;
                    Vector3 p2 = p1 + registerTransform.forward * (lineOffset - 1);
                    Handles.color = Color.green;
                    Handles.DrawDottedLine(p1, p2, 2);
                }

                // Body
                Handles.color = Color.white;
                Handles.DrawWireCube(pos - Vector3.up * 0.5f, Vector3.one);

                // Head
                DrawWireSphere(pos + Vector3.up * 0.5f, camForward, 0.5f);

                // Eyes
                Vector3 faceCenter = pos + Vector3.up * 0.6f + registerTransform.forward * 0.25f;
                Vector3 offset = registerTransform.right * 0.2f;
                DrawWireSphere(faceCenter + offset, camForward, 0.1f);
                DrawWireSphere(faceCenter - offset, camForward, 0.1f);
            }

        }

        void DrawWireSphere(Vector3 pos, Vector3 camForward, float radius)
        {
            Handles.DrawWireDisc(pos, camForward, radius);
            Handles.DrawWireDisc(pos, Vector3.up, radius);
            Handles.DrawWireDisc(pos, Vector3.right, radius);
        }
    }
}
