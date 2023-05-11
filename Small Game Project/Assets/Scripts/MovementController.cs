using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    //public Transform Orientation;
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private Vector3 _requiredForce;
    public OldCameraController cameraController;

    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _maxForce = 5f;
    [SerializeField] private float _sprintSpeed = 1.5f;

    private bool _isMoving;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _requiredForce = Vector3.zero;
        if (_isMoving)
        {
            Vector3 cameraDirection = cameraController.transform.forward;
            float speed = _speed;
            Vector3 currentDirection = transform.forward * _direction.y * cameraDirection.y;
            _requiredForce += currentDirection * (Mathf.Max(0.0f, speed - _rigidbody.velocity.magnitude) * _rigidbody.mass) / Time.fixedDeltaTime;


            Vector3 currentTurnDirection = transform.right * _direction.x;
            _requiredForce += currentTurnDirection * (Mathf.Max(0.0f, speed - _rigidbody.velocity.magnitude) * _rigidbody.mass) / Time.fixedDeltaTime;

        }

    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(Vector3.ClampMagnitude(_requiredForce, Mathf.Min(_requiredForce.magnitude, _maxForce)));
    }


    #region Input
    public void OnMove(InputAction.CallbackContext context)
    {
        //if (cameraController.manualCameraTimer > 0)
        //{
        //    _rigidbody.transform.forward = cameraController.transform.forward;
        //}
        _direction = context.ReadValue<Vector2>().normalized;
        if (context.ReadValue<Vector2>().normalized != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }
    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    bool isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);

    //    if (context.performed && isGrounded)
    //    {
    //        _rigidbody.AddForce(-_gravityBody.GravityDirection * _jumpForce, ForceMode.Impulse);

    //    }
    //}
    #endregion
}
