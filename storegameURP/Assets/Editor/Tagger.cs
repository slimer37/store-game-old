using UnityEngine;
using UnityEditor;

public class Tagger : EditorWindow
{
    private static string tagStr = "";
    private static string filterTag = "";

    [MenuItem("Window/Tagger")]
    static void Init() => GetWindow<Tagger>("Tag and Layer Manager");

    void OnGUI()
    {
        tagStr = EditorGUILayout.TagField("Tag for Objects:", tagStr);
        filterTag = EditorGUILayout.TagField("Filter:", filterTag);

        if (GUILayout.Button("Set Tag!"))
        { SetTags(); }
    }

    static void SetTags()
    {
        GameObject[] objs = filterTag == "" ? Selection.gameObjects : GameObject.FindGameObjectsWithTag(filterTag);
        foreach (GameObject go in objs)
        {
            go.tag = tagStr;
            Debug.Log($"Tagged {go.name} as {tagStr}", go);
        }
        Undo.RecordObjects(objs, "Set tags");
    }
}
