// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Controls/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7e97e9f3-9a4e-46ff-bc0e-bb6abb300dd9"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""83813b7e-dd29-40e8-bc1b-54c2c1d73863"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Secondary Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7b50ca0d-ad54-4c94-89eb-4b690f876215"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""830c6d0f-24f5-4f3f-8ad1-4347f6152a80"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""abc75141-4b7e-4f52-a809-ba8b344362a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b104e224-0ab3-49f6-8577-2db3eea95f35"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""bc5baf25-7800-4943-bc06-f12db0b28040"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shift Item"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4725529b-6091-4606-b1d5-179a14bc66e4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""785c9edc-91d6-4b92-828d-577ca88b970e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c7eefeba-3204-421b-a88c-972aa7c728e4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad76b156-af69-488e-a69c-365903e8f003"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""1c1d46f8-2ef6-47be-9c8f-a36e3c2585f6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e61cc84f-2239-4fd9-8a11-c26967118ba3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4cd606d9-dc90-4422-92c8-8a47127bc1db"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6e592d00-40c1-4a2a-af33-76ea545a0f8f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""52693474-800d-4bed-b7da-467593091afd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6aae2f79-a49f-4ef7-9dfe-b6c6ea766a1d"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f59b3ab2-6747-456f-aaeb-6d2913097ffa"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Secondary Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb9feda5-79db-40ff-8424-5d70e0dbce7d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Shift Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3fefafc-3dd4-4ca7-b408-e6401a06d26c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8ff0fae-ed1e-4ad4-ada1-d016d4d41148"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""9de0ccb3-df98-4cb5-ae4a-970d77ba032b"",
            ""actions"": [
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""7e947d23-569d-40c4-a30c-06da86476adb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Exit"",
                    ""type"": ""Button"",
                    ""id"": ""a636c21c-6122-4c7b-a3f0-73e2c25058bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""0310f950-9ecc-449b-a1a2-8b6670b8d2bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b13f5d71-4696-4a50-85be-7ed4e9b7ba35"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cab7cad2-6e91-4f87-8c97-1da13567a798"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7362249d-ca2b-4b0f-8e7b-d6d66aa25d50"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Exit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4d073b7-f448-46c1-a200-351fc4c82bdd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Console"",
            ""id"": ""2d5b931d-9008-49d5-9b46-73e98df31964"",
            ""actions"": [
                {
                    ""name"": ""Open"",
                    ""type"": ""Button"",
                    ""id"": ""b8a3e982-2a77-4939-8512-90ea77650eb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Last Command"",
                    ""type"": ""Button"",
                    ""id"": ""1658223b-3f3a-4008-bb1c-9d413b8e99b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""7d2d6364-850b-491e-8c59-9c955806cd6b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2ca1ce3-d3d1-4a05-b6c0-7d49034a3ee9"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Open"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3ec3aff-bd1d-44b4-a64d-5d94bc55d102"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Last Command"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23a7db31-3ac2-415e-bb99-9ee987789227"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        m_Player_SecondaryInteract = m_Player.FindAction("Secondary Interact", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_ShiftItem = m_Player.FindAction("Shift Item", throwIfNotFound: true);
        m_Player_Throw = m_Player.FindAction("Throw", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Select = m_Menu.FindAction("Select", throwIfNotFound: true);
        m_Menu_Exit = m_Menu.FindAction("Exit", throwIfNotFound: true);
        m_Menu_Pause = m_Menu.FindAction("Pause", throwIfNotFound: true);
        // Console
        m_Console = asset.FindActionMap("Console", throwIfNotFound: true);
        m_Console_Open = m_Console.FindAction("Open", throwIfNotFound: true);
        m_Console_LastCommand = m_Console.FindAction("Last Command", throwIfNotFound: true);
        m_Console_Submit = m_Console.FindAction("Submit", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Interact;
    private readonly InputAction m_Player_SecondaryInteract;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_ShiftItem;
    private readonly InputAction m_Player_Throw;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
        public InputAction @SecondaryInteract => m_Wrapper.m_Player_SecondaryInteract;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @ShiftItem => m_Wrapper.m_Player_ShiftItem;
        public InputAction @Throw => m_Wrapper.m_Player_Throw;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @SecondaryInteract.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryInteract;
                @SecondaryInteract.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryInteract;
                @SecondaryInteract.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSecondaryInteract;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @ShiftItem.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShiftItem;
                @ShiftItem.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShiftItem;
                @ShiftItem.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShiftItem;
                @Throw.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @SecondaryInteract.started += instance.OnSecondaryInteract;
                @SecondaryInteract.performed += instance.OnSecondaryInteract;
                @SecondaryInteract.canceled += instance.OnSecondaryInteract;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @ShiftItem.started += instance.OnShiftItem;
                @ShiftItem.performed += instance.OnShiftItem;
                @ShiftItem.canceled += instance.OnShiftItem;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Select;
    private readonly InputAction m_Menu_Exit;
    private readonly InputAction m_Menu_Pause;
    public struct MenuActions
    {
        private @Controls m_Wrapper;
        public MenuActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_Menu_Select;
        public InputAction @Exit => m_Wrapper.m_Menu_Exit;
        public InputAction @Pause => m_Wrapper.m_Menu_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSelect;
                @Exit.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnExit;
                @Exit.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnExit;
                @Exit.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnExit;
                @Pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Exit.started += instance.OnExit;
                @Exit.performed += instance.OnExit;
                @Exit.canceled += instance.OnExit;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Console
    private readonly InputActionMap m_Console;
    private IConsoleActions m_ConsoleActionsCallbackInterface;
    private readonly InputAction m_Console_Open;
    private readonly InputAction m_Console_LastCommand;
    private readonly InputAction m_Console_Submit;
    public struct ConsoleActions
    {
        private @Controls m_Wrapper;
        public ConsoleActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Open => m_Wrapper.m_Console_Open;
        public InputAction @LastCommand => m_Wrapper.m_Console_LastCommand;
        public InputAction @Submit => m_Wrapper.m_Console_Submit;
        public InputActionMap Get() { return m_Wrapper.m_Console; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ConsoleActions set) { return set.Get(); }
        public void SetCallbacks(IConsoleActions instance)
        {
            if (m_Wrapper.m_ConsoleActionsCallbackInterface != null)
            {
                @Open.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnOpen;
                @Open.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnOpen;
                @Open.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnOpen;
                @LastCommand.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnLastCommand;
                @LastCommand.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnLastCommand;
                @LastCommand.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnLastCommand;
                @Submit.started -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_ConsoleActionsCallbackInterface.OnSubmit;
            }
            m_Wrapper.m_ConsoleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Open.started += instance.OnOpen;
                @Open.performed += instance.OnOpen;
                @Open.canceled += instance.OnOpen;
                @LastCommand.started += instance.OnLastCommand;
                @LastCommand.performed += instance.OnLastCommand;
                @LastCommand.canceled += instance.OnLastCommand;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
            }
        }
    }
    public ConsoleActions @Console => new ConsoleActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnSecondaryInteract(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnShiftItem(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnExit(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IConsoleActions
    {
        void OnOpen(InputAction.CallbackContext context);
        void OnLastCommand(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
