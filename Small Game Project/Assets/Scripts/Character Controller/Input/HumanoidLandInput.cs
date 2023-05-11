using UnityEngine;
using UnityEngine.InputSystem;

public class HumanoidLandInput : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public bool moveIsPressed;
    public Vector2 lookInput { get; private set; }
    public bool invertMouseY { get; private set; } = true;
    public float zoomCameraInput { get; private set; }
    public bool invertScroll { get; private set; }
    public bool sprintIsPressed { get; private set; }
    public bool jumpIsPressed { get; private set; }
    public bool changeCameraWasPressedThisFrame { get; private set; }
    public bool devToolsPressed { get; private set; }

    InputActions _input;

    private void OnEnable()
    {
        _input = new InputActions();
        _input.HumonoidLand.Enable();

        _input.HumonoidLand.Move.performed += SetMove;
        _input.HumonoidLand.Move.canceled += SetMove;

        _input.HumonoidLand.Look.performed += SetLook;
        _input.HumonoidLand.Look.canceled += SetLook;

        _input.HumonoidLand.Sprint.started += SetSprint;
        _input.HumonoidLand.Sprint.canceled += SetSprint;

        _input.HumonoidLand.Jump.started += SetJump;
        _input.HumonoidLand.Jump.canceled += SetJump;

        _input.HumonoidLand.ZoomCamera.started += SetZoomCamera;
        _input.HumonoidLand.ZoomCamera.canceled += SetZoomCamera;
    }

    private void OnDisable()
    {
        _input.HumonoidLand.Move.performed -= SetMove;
        _input.HumonoidLand.Move.canceled -= SetMove;

        _input.HumonoidLand.Look.performed -= SetLook;
        _input.HumonoidLand.Look.canceled -= SetLook;

        _input.HumonoidLand.Sprint.started -= SetSprint;
        _input.HumonoidLand.Sprint.canceled -= SetSprint;

        _input.HumonoidLand.Jump.started -= SetJump;
        _input.HumonoidLand.Jump.canceled -= SetJump;

        _input.HumonoidLand.ZoomCamera.started -= SetZoomCamera;
        _input.HumonoidLand.ZoomCamera.canceled -= SetZoomCamera;

        _input.HumonoidLand.Disable();
    }

    private void Update()
    {
        changeCameraWasPressedThisFrame = _input.HumonoidLand.ChangeCamera.WasPressedThisFrame();
        devToolsPressed = _input.HumonoidLand.DevPurposes.WasPressedThisFrame();
    }

    void SetMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveIsPressed = !(moveInput == Vector2.zero);
    }

    void SetLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    void SetSprint(InputAction.CallbackContext context)
    {
        sprintIsPressed = context.started;
    }

    void SetJump(InputAction.CallbackContext context)
    {
        jumpIsPressed = context.started;
    }

    void SetZoomCamera(InputAction.CallbackContext context)
    {
        zoomCameraInput = context.ReadValue<float>();
    }
}
