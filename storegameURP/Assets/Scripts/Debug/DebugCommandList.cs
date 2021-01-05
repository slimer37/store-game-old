using System;
using System.Collections.Generic;
using UnityEngine;

public static class DebugCommands
{
    private static string command;

    private static readonly string helpString =
        Colored("yellow", "help") + " - Lists all commands.\n" +
        Colored("yellow", "allthings") + " - Lists all objects in the scene.\n" +
        Colored("yellow", "topthings") + " - Lists all top-level objects in the scene.\n" +
        Colored("yellow", "childrenof") + Colored("orange", " [object name]") + " - Lists an object's children.\n" +
        Colored("yellow", "destroy") + Colored("orange", " [object name]") + " - Destroys an object.\n" +
        Colored("yellow", "reload") + " - Reloads the scene.\n" +
        Colored("yellow", "clear") + " - Clears the console output.";

    private static Exception NoArgs() => new Exception($"'{command}' requires at least one argument. (Consult the help command.)");

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
            "help" => helpString,
            "allthings" => GetObjectList(),
            "topthings" => GetParents(),
            "childrenof" => ChildrenOf(singleArg),
            "destroy" => DestroyObject(singleArg),
            "reload" => ReloadScene(),
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
        string list = "";
        Transform[] hierarchy = GameObject.Find(objName).transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < hierarchy.Length; i++)
        { list += ", " + hierarchy[i].name; }
        return Colored("yellow", $"Children of '{hierarchy[0].name}': ") + list.Substring(2);
    }

    public static string DestroyObject(string name)
    {
        if (name == "")
        { throw NoArgs(); }

        string result = "";

        if (GameObject.Find(name) == null)
        { result += "\n" + Colored("red", $"GameObject {name} could not be found."); }
        else if (name == "Console")
        { result += "\n" + Colored("yellow", "You can't destroy the console!"); }
        else
        {
            UnityEngine.Object.Destroy(GameObject.Find(name));
            result += $"\nDestroyed {name}.";
        }

        return result.Substring(1);
    }

    public static string ReloadScene()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        return "Success.";
    }
}
