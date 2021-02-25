using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomerSpawner))]
public class CustomerSpawnerEditor : Editor
{
    private static SerializedObject spawnerObj = null;
    private static bool pressed = false;

    void Awake() => spawnerObj = serializedObject;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        pressed = GUILayout.Toggle(pressed, "Edit Points", "Button");
        base.OnInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        { SceneView.RepaintAll(); }
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmos(CustomerSpawner spawner, GizmoType type)
    {
        DrawSpheres(ObtainArray("spawnPositions"), Color.green);
        DrawSpheres(ObtainArray("endPositions"), Color.red);

        void DrawSpheres(Vector3[] positions, Color color)
        {
            color.a = pressed ? 0.5f : 1;
            Gizmos.color = color;
            foreach (var pos in positions)
            { Gizmos.DrawSphere(pos, 0.5f); }
        }
    }

    void OnSceneGUI()
    {
        if (!pressed) return;

        DrawHandles("spawnPositions", 1);
        DrawHandles("endPositions", 2.5f);
        serializedObject.ApplyModifiedProperties();

        void DrawHandles(string name, float forceHeight)
        {
            Vector3[] positions = ObtainArray(name);
            SerializedProperty property = spawnerObj.FindProperty(name);
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
                positions[i].y = forceHeight;
                property.GetArrayElementAtIndex(i).vector3Value = positions[i];
            }
        }
    }

    static Vector3[] ObtainArray(string serializedName)
    {
        if (spawnerObj == null)
        { return new Vector3[] { }; }

        SerializedProperty property = spawnerObj.FindProperty(serializedName);
        Vector3[] array = new Vector3[property.arraySize];
        for (int i = 0; i < property.arraySize; i++)
        { array[i] = property.GetArrayElementAtIndex(i).vector3Value; }
        return array;
    }
}
