//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/EasyCustomCursor/Input/CustomCursorInputs.inputactions
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

public partial class @CustomCursorInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CustomCursorInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CustomCursorInputs"",
    ""maps"": [
        {
            ""name"": ""CustomCursor"",
            ""id"": ""a980fff6-23f8-40c3-8c04-d287cd85c454"",
            ""actions"": [
                {
                    ""name"": ""LeftClicking"",
                    ""type"": ""Button"",
                    ""id"": ""56bcdab5-0a66-426d-9be4-76c3e5f5196a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClicking"",
                    ""type"": ""Button"",
                    ""id"": ""cdc4ac72-903a-4410-a7f7-7725b2444f20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollClicking"",
                    ""type"": ""Button"",
                    ""id"": ""e9964703-a3e1-47cc-bb70-01b280735325"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scrolling"",
                    ""type"": ""Value"",
                    ""id"": ""4991527a-3a69-4dc6-8f3f-a9b19a9b005b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e97148ee-fde6-47bd-97c4-2787d1753c93"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""LeftClicking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fcc3086-c006-4be2-b0f2-8e3284332b71"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""RightClicking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85d9aa88-ce59-4f80-9131-2935c168dab6"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""ScrollClicking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f330ba3f-dd96-4ab2-8aac-0f406e104e59"",
                    ""path"": ""<Mouse>/Scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse"",
                    ""action"": ""Scrolling"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse"",
            ""bindingGroup"": ""Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CustomCursor
        m_CustomCursor = asset.FindActionMap("CustomCursor", throwIfNotFound: true);
        m_CustomCursor_LeftClicking = m_CustomCursor.FindAction("LeftClicking", throwIfNotFound: true);
        m_CustomCursor_RightClicking = m_CustomCursor.FindAction("RightClicking", throwIfNotFound: true);
        m_CustomCursor_ScrollClicking = m_CustomCursor.FindAction("ScrollClicking", throwIfNotFound: true);
        m_CustomCursor_Scrolling = m_CustomCursor.FindAction("Scrolling", throwIfNotFound: true);
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

    // CustomCursor
    private readonly InputActionMap m_CustomCursor;
    private List<ICustomCursorActions> m_CustomCursorActionsCallbackInterfaces = new List<ICustomCursorActions>();
    private readonly InputAction m_CustomCursor_LeftClicking;
    private readonly InputAction m_CustomCursor_RightClicking;
    private readonly InputAction m_CustomCursor_ScrollClicking;
    private readonly InputAction m_CustomCursor_Scrolling;
    public struct CustomCursorActions
    {
        private @CustomCursorInputs m_Wrapper;
        public CustomCursorActions(@CustomCursorInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClicking => m_Wrapper.m_CustomCursor_LeftClicking;
        public InputAction @RightClicking => m_Wrapper.m_CustomCursor_RightClicking;
        public InputAction @ScrollClicking => m_Wrapper.m_CustomCursor_ScrollClicking;
        public InputAction @Scrolling => m_Wrapper.m_CustomCursor_Scrolling;
        public InputActionMap Get() { return m_Wrapper.m_CustomCursor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CustomCursorActions set) { return set.Get(); }
        public void AddCallbacks(ICustomCursorActions instance)
        {
            if (instance == null || m_Wrapper.m_CustomCursorActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CustomCursorActionsCallbackInterfaces.Add(instance);
            @LeftClicking.started += instance.OnLeftClicking;
            @LeftClicking.performed += instance.OnLeftClicking;
            @LeftClicking.canceled += instance.OnLeftClicking;
            @RightClicking.started += instance.OnRightClicking;
            @RightClicking.performed += instance.OnRightClicking;
            @RightClicking.canceled += instance.OnRightClicking;
            @ScrollClicking.started += instance.OnScrollClicking;
            @ScrollClicking.performed += instance.OnScrollClicking;
            @ScrollClicking.canceled += instance.OnScrollClicking;
            @Scrolling.started += instance.OnScrolling;
            @Scrolling.performed += instance.OnScrolling;
            @Scrolling.canceled += instance.OnScrolling;
        }

        private void UnregisterCallbacks(ICustomCursorActions instance)
        {
            @LeftClicking.started -= instance.OnLeftClicking;
            @LeftClicking.performed -= instance.OnLeftClicking;
            @LeftClicking.canceled -= instance.OnLeftClicking;
            @RightClicking.started -= instance.OnRightClicking;
            @RightClicking.performed -= instance.OnRightClicking;
            @RightClicking.canceled -= instance.OnRightClicking;
            @ScrollClicking.started -= instance.OnScrollClicking;
            @ScrollClicking.performed -= instance.OnScrollClicking;
            @ScrollClicking.canceled -= instance.OnScrollClicking;
            @Scrolling.started -= instance.OnScrolling;
            @Scrolling.performed -= instance.OnScrolling;
            @Scrolling.canceled -= instance.OnScrolling;
        }

        public void RemoveCallbacks(ICustomCursorActions instance)
        {
            if (m_Wrapper.m_CustomCursorActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICustomCursorActions instance)
        {
            foreach (var item in m_Wrapper.m_CustomCursorActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CustomCursorActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CustomCursorActions @CustomCursor => new CustomCursorActions(this);
    private int m_MouseSchemeIndex = -1;
    public InputControlScheme MouseScheme
    {
        get
        {
            if (m_MouseSchemeIndex == -1) m_MouseSchemeIndex = asset.FindControlSchemeIndex("Mouse");
            return asset.controlSchemes[m_MouseSchemeIndex];
        }
    }
    public interface ICustomCursorActions
    {
        void OnLeftClicking(InputAction.CallbackContext context);
        void OnRightClicking(InputAction.CallbackContext context);
        void OnScrollClicking(InputAction.CallbackContext context);
        void OnScrolling(InputAction.CallbackContext context);
    }
}