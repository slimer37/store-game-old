using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DebugCommands
{
    private static string command;

    private static readonly string helpString =
        Colored("yellow", "allthings") + " - Lists all objects in the scene.\n" +
        Colored("yellow", "childrenof") + Colored("orange", " [object name]") + " - Lists an object's children.\n" +
        Colored("yellow", "clear") + " - Clears the console output.\n" +
        Colored("yellow", "destroy") + Colored("orange", " [object name]") + " - Destroys an object.\n" +
        Colored("yellow", "help") + " - Lists all commands.\n" +
        Colored("yellow", "reload") + " - Reloads the scene.\n" +
        Colored("yellow", "topthings") + " - Lists all top-level objects in the scene.";

    private static void ArgCheck(string arg)
    {
        if (arg == "")
        { throw new Exception($"'{command}' requires at least one argument. (Consult the help command.)"); }
    }

    private static string Colored(string color, string text) => $"<color={color}>{text}</color>";

    public static string Process(string input)
    {
        List<string> words = new List<string>();
        words.AddRange(input.Split(' '));
        command = words[0];
        words.RemoveAt(0);
        string[] args = words.ToArray();
        string singleArg = string.Join(" ", args);

        return command switch
        {
            "allthings" => GetObjectList(),
            "childrenof" => ChildrenOf(singleArg),
            "destroy" => DestroyObject(singleArg),
            "help" => helpString,
            "reload" => ReloadScene(),
            "topthings" => GetParents(),
            _ => throw new Exception($"The command '{command}' does not exist.")
        };
    }

    public static string GetObjectList()
    {
        string list = "";
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        { list += ", " + go.name; }
        return Colored("yellow", "Found: ") + list.Substring(2);
    }

    public static string GetParents()
    {
        string list = "";
        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (go.transform.parent == null)
            { list += ", " + go.name; }
        }
        return Colored("yellow", "Parents: ") + list.Substring(2);
    }

    public static string ChildrenOf(string objName)
    {
        ArgCheck(objName);

        if (!GameObject.Find(objName))
        { throw new Exception($"'{objName}' not found."); }

        Transform[] hierarchy = GameObject.Find(objName).transform.GetComponentsInChildren<Transform>();

        if (hierarchy.Length == 1)
        { return Colored("yellow", $"'{objName}' has no children."); }

        string list = "";

        for (int i = 1; i < hierarchy.Length; i++)
        { list += ", " + hierarchy[i].name; }
        return Colored("yellow", $"Children of '{objName}': ") + list.Substring(2);
    }

    public static string DestroyObject(string objName)
    {
        ArgCheck(objName);

        if (!GameObject.Find(objName))
        { throw new Exception($"'{objName}' not found."); }
        else if (objName == "Console")
        { return Colored("yellow", "You can't destroy the console!"); }
        else
        {
            UnityEngine.Object.Destroy(GameObject.Find(objName));
            return $"Destroyed '{objName}'.";
        }
    }

    public static string ReloadScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
        return $"Successfully reloaded scene '{activeScene.name}'.";
    }
}
