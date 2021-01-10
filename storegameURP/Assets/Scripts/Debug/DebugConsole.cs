using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DebugConsole : MonoBehaviour
{
    [SerializeField] private string cheatCode;
    [SerializeField] private GameObject notifObject;
    [SerializeField] private bool open;

    [Header("Text")]
    [SerializeField] private Font outputFont;
    [SerializeField] private Font inputFont;
    [SerializeField] private int outputFontSize;
    [SerializeField] private int inputFontSize;

    private static DebugConsole current;

    private bool debug = false;
    private int codeProgress = 0;
    private Controls controls;

    private string input = "";
    private string output = "Wizards only, fools.";
    private Vector2 scrollPosition;

    private string memory;

    void Awake()
    {
        if (current)
        { Destroy(gameObject); }
        else
        { current = this; }

        var kb = Keyboard.current;
        kb.onTextInput += EnterCheatCode;

        controls = new Controls();
        controls.Enable();
        controls.Console.Open.performed += _ => open = debug ? !open : open;
        controls.Console.Submit.performed += _ => Submit();
        controls.Console.LastCommand.performed += _ => RecallCommand();

        DontDestroyOnLoad(gameObject);
    }

    void RecallCommand() => input = memory;

    void Submit()
    {
        if (input != "")
        {
            if (output != "")
            { Append("\n"); }

            if (input == "clear")
            { output = ""; }
            else
            {
                try
                { Append(DebugCommands.Process(input)); }
                catch (System.Exception e)
                { Append($"<color=red>Error: {e.Message}</color>"); }
            }

            memory = input;
            input = "";
        }

        void Append(string text) => output = text + output;
    }

    void OnGUI()
    {
        if (open)
        {
            GUI.skin.textArea.fontSize = outputFontSize;
            GUI.skin.textField.fontSize = inputFontSize;
            GUI.skin.textArea.font = outputFont;
            GUI.skin.textField.font = inputFont;

            GUI.skin.textArea.richText = true;

            input = GUILayout.TextField(input,
                GUILayout.Width(Screen.width), GUILayout.MinHeight(20), GUILayout.ExpandHeight(true));

            scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                GUILayout.Width(Screen.width), GUILayout.Height(200));
            GUILayout.TextArea(output);
            GUILayout.EndScrollView();
        }
    }

    void EnterCheatCode(char letter)
    {
        if (!debug && SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (letter == cheatCode[codeProgress])
            { codeProgress++; }
            else
            { codeProgress = 0; }

            if (codeProgress == cheatCode.Length)
            {
                debug = true;

                if (notifObject)
                { notifObject.SetActive(true); }
            }
        }
    }
}
