using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DebugCommands
{
    struct Command
    {
        public string keyword;
        public string param;
        public string desc;
        public readonly string HelpString => Colored("yellow", keyword) + (param != "" ? Colored("orange", $" [{param}]") : "") + " - " + desc;

        readonly Func<string, string> function;
        readonly bool paramRequired;

        public Command(string keyword, string desc, string param, bool paramRequired, Func<string, string> function)
        {
            this.keyword = keyword;
            this.desc = desc;
            this.param = param;
            this.paramRequired = !string.IsNullOrEmpty(param) && paramRequired;
            this.function = function;
        }

        public Command(string keyword, string desc, Func<string, string> function)
        {
            this.keyword = keyword;
            this.desc = desc;
            param = "";
            paramRequired = false;
            this.function = function;
        }

        public string Execute(string arg)
        {
            if (paramRequired && string.IsNullOrWhiteSpace(arg))
            { throw new Exception($"'{keyword}' requires at least one argument. (Consult the help command.)"); }
            return function(arg);
        }
    }

    static string keyword;

    static string Colored(string color, string text) => $"<color={color}>{text}</color>";

    public static string Process(string input)
    {
        List<string> words = new List<string>();
        words.AddRange(input.Split(' '));
        keyword = words[0];
        words.RemoveAt(0);
        string[] args = words.ToArray();
        string combinedArgs = string.Join(" ", args);

        foreach (var command in commands)
        {
            if (keyword == command.keyword)
            { return command.Execute(combinedArgs); }
        }

        throw new Exception($"The command '{keyword}' does not exist.");
    }

    static readonly Command[] commands =
    {
        new Command("allthings", "Lists all objects in the scene.", _ =>
            {
                string list = "";
                foreach (var go in GameObject.FindObjectsOfType<GameObject>())
                { list += ", " + go.name; }
                return Colored("yellow", "Found: ") + list.Substring(2);
            }),

        new Command("childrenof", "Lists an object's children.", "object name", true, objName =>
            {
                if (!GameObject.Find(objName))
                { throw new Exception($"'{objName}' not found."); }

                Transform[] hierarchy = GameObject.Find(objName).transform.GetComponentsInChildren<Transform>();

                if (hierarchy.Length == 1)
                { return Colored("yellow", $"'{objName}' has no children."); }

                string list = "";

                for (int i = 1; i < hierarchy.Length; i++)
                { list += ", " + hierarchy[i].name; }
                return Colored("yellow", $"Children of '{objName}': ") + list.Substring(2);
            }),

        new Command("clear", "Clears the console output.", _ => ""),

        new Command("destroy", "Destroys an object.", "object name", true, objName =>
            {
                if (!GameObject.Find(objName))
                { throw new Exception($"'{objName}' not found."); }
                else if (objName == "Console")
                { return Colored("yellow", "You can't destroy the console!"); }
                else
                {
                    UnityEngine.Object.Destroy(GameObject.Find(objName));
                    return $"Destroyed '{objName}'.";
                }
            }),

        new Command( "help", "Lists all commands.", _ =>
            {
                string fullHelpString = "";
                foreach (var command in commands)
                { fullHelpString += command.HelpString + "\n"; }

                // Ditch the final newline.
                return fullHelpString.Substring(0, fullHelpString.Length - 1);
            }),

        new Command( "reload", "Reloads the scene.", _ =>
            {
                Scene activeScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(activeScene.buildIndex);
                return $"Successfully reloaded scene '{activeScene.name}'.";
            }),

        new Command("topthings", "Lists all top-level objects in the scene.", _ =>
            {
                string list = "";
                foreach (var go in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (go.transform.parent == null)
                    { list += ", " + go.name; }
                }
                return Colored("yellow", "Parents: ") + list.Substring(2);
            }),
    };
}
