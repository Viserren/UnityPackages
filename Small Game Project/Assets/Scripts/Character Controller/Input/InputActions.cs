//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/Character Controller/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Humonoid Land"",
            ""id"": ""82ee32d2-2275-4454-9252-969f7b0b5d0a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0d1f8587-dbc0-42b3-a26b-50ae7f2e4d50"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""eb3e032c-e209-4434-be98-808d7428034e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Change Camera"",
                    ""type"": ""Button"",
                    ""id"": ""3f1eea37-8265-4b27-b9fb-098bb0f6dd06"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom Camera"",
                    ""type"": ""Value"",
                    ""id"": ""7fd4ffe4-f594-487c-9f00-291ded0afc29"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""c7f8ef7c-9888-4b8b-9005-681d31189a9e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""8183a2eb-c514-45a4-acbb-677cd078634f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dev Purposes"",
                    ""type"": ""Button"",
                    ""id"": ""cc335610-c214-49ce-8671-86afb9b040f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""eacf6a7d-c75e-4c5d-8bd4-f8e1cad5d935"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""92c68be4-2a33-4d19-a1d9-25fb4c53a469"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e7aab135-fcad-4af9-a2a4-b3632bbba61d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""27e37177-971e-4e7f-93b6-5de29d6a8672"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""295611d3-ccb6-422d-8904-99c6a33a8acf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4306c393-5055-4602-8a00-484e5835c13c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ff68a89f-bd8d-436d-83ee-7bc35647cc76"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ea1da59-29b7-48d7-90dc-b63ba15d6a66"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.03,y=0.03)"",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1aff3307-5496-4e81-af53-e0cccb4c3ab4"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Change Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afba2a17-208a-4a87-86f0-07c0deb8bf67"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Change Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4bf9e30-fe82-4e5f-95cc-809a0c15bab4"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Zoom Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8bb0806-322c-4dee-afa9-9d62f0274734"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-4,max=4)"",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Zoom Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d34137e-94f6-4960-afe9-ff2ea0a21b7b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1cbbecf-323a-472c-88f3-6af2bb701bd8"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f9df493-9751-4f29-b1c9-09818e272bbe"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""760955f5-424d-4ede-90eb-de3d58bae178"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae7183fc-2cfa-42db-bd19-ddc884809e75"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Dev Purposes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
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
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Humonoid Land
        m_HumonoidLand = asset.FindActionMap("Humonoid Land", throwIfNotFound: true);
        m_HumonoidLand_Move = m_HumonoidLand.FindAction("Move", throwIfNotFound: true);
        m_HumonoidLand_Look = m_HumonoidLand.FindAction("Look", throwIfNotFound: true);
        m_HumonoidLand_ChangeCamera = m_HumonoidLand.FindAction("Change Camera", throwIfNotFound: true);
        m_HumonoidLand_ZoomCamera = m_HumonoidLand.FindAction("Zoom Camera", throwIfNotFound: true);
        m_HumonoidLand_Sprint = m_HumonoidLand.FindAction("Sprint", throwIfNotFound: true);
        m_HumonoidLand_Jump = m_HumonoidLand.FindAction("Jump", throwIfNotFound: true);
        m_HumonoidLand_DevPurposes = m_HumonoidLand.FindAction("Dev Purposes", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Humonoid Land
    private readonly InputActionMap m_HumonoidLand;
    private IHumonoidLandActions m_HumonoidLandActionsCallbackInterface;
    private readonly InputAction m_HumonoidLand_Move;
    private readonly InputAction m_HumonoidLand_Look;
    private readonly InputAction m_HumonoidLand_ChangeCamera;
    private readonly InputAction m_HumonoidLand_ZoomCamera;
    private readonly InputAction m_HumonoidLand_Sprint;
    private readonly InputAction m_HumonoidLand_Jump;
    private readonly InputAction m_HumonoidLand_DevPurposes;
    public struct HumonoidLandActions
    {
        private @InputActions m_Wrapper;
        public HumonoidLandActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_HumonoidLand_Move;
        public InputAction @Look => m_Wrapper.m_HumonoidLand_Look;
        public InputAction @ChangeCamera => m_Wrapper.m_HumonoidLand_ChangeCamera;
        public InputAction @ZoomCamera => m_Wrapper.m_HumonoidLand_ZoomCamera;
        public InputAction @Sprint => m_Wrapper.m_HumonoidLand_Sprint;
        public InputAction @Jump => m_Wrapper.m_HumonoidLand_Jump;
        public InputAction @DevPurposes => m_Wrapper.m_HumonoidLand_DevPurposes;
        public InputActionMap Get() { return m_Wrapper.m_HumonoidLand; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HumonoidLandActions set) { return set.Get(); }
        public void SetCallbacks(IHumonoidLandActions instance)
        {
            if (m_Wrapper.m_HumonoidLandActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnLook;
                @ChangeCamera.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnChangeCamera;
                @ChangeCamera.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnChangeCamera;
                @ZoomCamera.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnZoomCamera;
                @ZoomCamera.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnZoomCamera;
                @ZoomCamera.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnZoomCamera;
                @Sprint.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnJump;
                @DevPurposes.started -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnDevPurposes;
                @DevPurposes.performed -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnDevPurposes;
                @DevPurposes.canceled -= m_Wrapper.m_HumonoidLandActionsCallbackInterface.OnDevPurposes;
            }
            m_Wrapper.m_HumonoidLandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @ChangeCamera.started += instance.OnChangeCamera;
                @ChangeCamera.performed += instance.OnChangeCamera;
                @ChangeCamera.canceled += instance.OnChangeCamera;
                @ZoomCamera.started += instance.OnZoomCamera;
                @ZoomCamera.performed += instance.OnZoomCamera;
                @ZoomCamera.canceled += instance.OnZoomCamera;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @DevPurposes.started += instance.OnDevPurposes;
                @DevPurposes.performed += instance.OnDevPurposes;
                @DevPurposes.canceled += instance.OnDevPurposes;
            }
        }
    }
    public HumonoidLandActions @HumonoidLand => new HumonoidLandActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IHumonoidLandActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnChangeCamera(InputAction.CallbackContext context);
        void OnZoomCamera(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDevPurposes(InputAction.CallbackContext context);
    }
}