using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DebugCommands
{
    delegate string CommandFunction(string input);

    struct Command
    {
        public string keyword;
        public string param;
        public string desc;
        public CommandFunction function;
        public readonly string HelpString => Colored("yellow", keyword) + (param != "" ? Colored("orange", $" [{param}]") : "") + " - " + desc;

        public Command(string keyword, string desc, CommandFunction function, string param = "")
        {
            this.keyword = keyword;
            this.param = param;
            this.desc = desc;
            this.function = function;
        }
    }

    static string keyword;

    static readonly Command[] commands =
    {
        new Command("allthings", "Lists all objects in the scene.", _ => GetObjectList()),
        new Command("childrenof", "Lists an object's children.", ChildrenOf, "object name"),
        new Command("clear", "Clears the console output.", _ => ""),
        new Command("destroy", "Destroys an object.", DestroyObject, "object name"),
        new Command("help", "Lists all commands.", _ => GetHelpString()),
        new Command("reload", "Reloads the scene.", _ => ReloadScene()),
        new Command("topthings", "Lists all top-level objects in the scene.", _ => GetParents()),
    };

    static void ArgCheck(string arg)
    {
        if (arg == "")
        { throw new Exception($"'{keyword}' requires at least one argument. (Consult the help command.)"); }
    }

    static string Colored(string color, string text) => $"<color={color}>{text}</color>";

    public static string Process(string input)
    {
        List<string> words = new List<string>();
        words.AddRange(input.Split(' '));
        keyword = words[0];
        words.RemoveAt(0);
        string[] args = words.ToArray();
        string singleArg = string.Join(" ", args);

        foreach (var command in commands)
        {
            if (keyword == command.keyword)
            { return command.function(singleArg); }
        }
        throw new Exception($"The command '{keyword}' does not exist.");
    }

    public static string GetHelpString()
    {
        string fullHelpString = "";
        foreach (var command in commands)
        { fullHelpString += command.HelpString + "\n"; }

        // Ditch the final newline.
        return fullHelpString.Substring(0, fullHelpString.Length - 1);
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
