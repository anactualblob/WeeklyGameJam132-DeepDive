// GENERATED AUTOMATICALLY FROM 'Assets/Input/DefaultInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultInputActions : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @DefaultInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerActions"",
            ""id"": ""66280226-182c-470f-aa0e-b14c49b696e0"",
            ""actions"": [
                {
                    ""name"": ""DiveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""a93acc43-18f3-4901-81c6-f625f0f6bd2c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DiveRight"",
                    ""type"": ""Button"",
                    ""id"": ""d0406fa7-b697-4f22-9561-925d43cb640e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5776f85a-177b-4d93-bda5-068d7e0aba03"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DiveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd010eaa-0ed3-451a-8877-e6a2cc5bd306"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DiveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_DiveLeft = m_PlayerActions.FindAction("DiveLeft", throwIfNotFound: true);
        m_PlayerActions_DiveRight = m_PlayerActions.FindAction("DiveRight", throwIfNotFound: true);
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

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_DiveLeft;
    private readonly InputAction m_PlayerActions_DiveRight;
    public struct PlayerActionsActions
    {
        private @DefaultInputActions m_Wrapper;
        public PlayerActionsActions(@DefaultInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @DiveLeft => m_Wrapper.m_PlayerActions_DiveLeft;
        public InputAction @DiveRight => m_Wrapper.m_PlayerActions_DiveRight;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @DiveLeft.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveLeft;
                @DiveLeft.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveLeft;
                @DiveLeft.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveLeft;
                @DiveRight.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveRight;
                @DiveRight.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveRight;
                @DiveRight.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnDiveRight;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @DiveLeft.started += instance.OnDiveLeft;
                @DiveLeft.performed += instance.OnDiveLeft;
                @DiveLeft.canceled += instance.OnDiveLeft;
                @DiveRight.started += instance.OnDiveRight;
                @DiveRight.performed += instance.OnDiveRight;
                @DiveRight.canceled += instance.OnDiveRight;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerActionsActions
    {
        void OnDiveLeft(InputAction.CallbackContext context);
        void OnDiveRight(InputAction.CallbackContext context);
    }
}
