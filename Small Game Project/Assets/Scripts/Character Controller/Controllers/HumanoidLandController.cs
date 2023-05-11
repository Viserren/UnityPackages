using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidLandController : MonoBehaviour
{
    public Transform cameraFollow;
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] CameraController _cameraController;
    Rigidbody _rigidbody;
    CapsuleCollider _capsuleColider;

    Vector3 _playerMoveInput;

    Vector3 _playerLookInput;
    Vector3 _previousPlayerLookInput;
    float _cameraPitch;
    [SerializeField] float _playerLookInputLerpTime = 0.35f;

    [Header("Movement")]
    [SerializeField] float _movementMultiplier = 30.0f;
    [SerializeField] float _movementInAirMultiplier = .5f;
    [SerializeField] float _sprintMultiplier = 2.5f;
    [SerializeField][Range(0, 4)] float _rotationSpeedMultiplier = 1.8f;
    [SerializeField][Range(0, 4)] float _pitchSpeedMultiplier = 1.8f;
    [SerializeField][Range(0, 90)] float _maxSlopeAngle = 47.5f;

    [Header("Ground Check")]
    [SerializeField] bool _isPlayerGrounded = true;
    [SerializeField][Range(0, 1.8f)] float _groundCheckRadiusMultiplier = .9f;
    [SerializeField][Range(-.95f, 1.05f)] float _groundCheckDistance = .05f;
    RaycastHit _groundCheckHit = new RaycastHit();

    [Header("Gravity")]
    [SerializeField] float _gravityFallCurrent = -100;
    [SerializeField] float _gravityFallMin = -100;
    [SerializeField] float _gravityFallMax = -100;
    [SerializeField][Range(-5, -35)] float _gravityFallIncrementAmount = -20;
    [SerializeField] float _gravityFallIncrementTime = .05f;
    [SerializeField] float _playerFallTimer;
    [SerializeField] float _gravityGrounded = -1;

    [Header("Jumping")]
    [SerializeField] float _initialJumpForce = 750f;
    [SerializeField] float _continualJumpForceMultiplier = .1f;
    [SerializeField] float _jumpTime = .175f;
    [SerializeField] float _jumpTimeCounter;
    [SerializeField] float _coyoteTime = .15f;
    [SerializeField] float _coyoteTimeCounter;
    [SerializeField] float _jumpBufferTime = .2f;
    [SerializeField] float _jumpBufferTimeCounter;
    [SerializeField] bool _isPlayerJumping;
    [SerializeField] bool _canHoldSpace = true;
    [SerializeField] bool _jumpWasPressedLastFrame;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleColider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!_cameraController.usingOrbitalCamera)
        {
            _playerLookInput = GetLookInput();
            PlayerLook();
            PitchCamera();
        }

        _playerMoveInput = GetMoveInput();
        _isPlayerGrounded = PlayerGroundCheck();

        _playerMoveInput = PlayerMove();
        _playerMoveInput = PlayerSlope();
        _playerMoveInput = PlayerSprint();

        _playerMoveInput.y = PlayerFallGravity();
        _playerMoveInput.y = PlayerJump();

        _playerMoveInput *= _rigidbody.mass; // NOTE: For dev purposes

        _rigidbody.AddRelativeForce(_playerMoveInput, ForceMode.Force);
    }

    private Vector3 GetLookInput()
    {
        _previousPlayerLookInput = _playerLookInput;
        _playerLookInput = new Vector3(_input.lookInput.x, (_input.invertMouseY ? -_input.lookInput.y : _input.lookInput.y), 0);
        return Vector3.Lerp(_previousPlayerLookInput, _playerLookInput * Time.deltaTime, _playerLookInputLerpTime);
    }

    private void PlayerLook()
    {
        _rigidbody.rotation = Quaternion.Euler(0, _rigidbody.rotation.eulerAngles.y + (_playerLookInput.x * (_rotationSpeedMultiplier * 100)), 0);
    }

    private void PitchCamera()
    {
        Vector3 rotationValues = cameraFollow.rotation.eulerAngles;
        _cameraPitch += _playerLookInput.y * (_pitchSpeedMultiplier * 100);
        _cameraPitch = Mathf.Clamp(_cameraPitch, -89.9f, 89.9f);

        cameraFollow.rotation = Quaternion.Euler(_cameraPitch, rotationValues.y, rotationValues.z);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(_input.moveInput.x, 0, _input.moveInput.y);
    }

    private bool PlayerGroundCheck()
    {
        float sphereCastRadius = _capsuleColider.radius * _groundCheckRadiusMultiplier;
        float sphereCastTravelDistance = _capsuleColider.bounds.extents.y * _groundCheckDistance;
        return Physics.SphereCast(_rigidbody.position, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance);
    }
    private Vector3 PlayerMove()
    {
        return ((_isPlayerGrounded) ? (_playerMoveInput * _movementMultiplier) : (_playerMoveInput * _movementMultiplier * _movementInAirMultiplier));
        //return new Vector3(_playerMoveInput.x * _movementMultiplier, _playerMoveInput.y, _playerMoveInput.z * _movementMultiplier);
    }

    private Vector3 PlayerSlope()
    {
        Vector3 calculatedPlayerMovement = _playerMoveInput;

        if (_isPlayerGrounded)
        {
            Vector3 localGroundCheckHitNormal = _rigidbody.transform.InverseTransformDirection(_groundCheckHit.normal);

            float groundSlopeAngle = Vector3.Angle(localGroundCheckHitNormal, _rigidbody.transform.up);
            if (groundSlopeAngle == 0) // On flat land
            {
                if (_input.moveIsPressed)
                {
                    RaycastHit rayHit;
                    float rayHeightFromGround = .1f;
                    float rayCalculationRayHeight = _rigidbody.position.y - _capsuleColider.bounds.extents.y + rayHeightFromGround;
                    Vector3 rayOrigin = new Vector3(_rigidbody.position.x, rayCalculationRayHeight, _rigidbody.position.z);
                    if (Physics.Raycast(rayOrigin, _rigidbody.transform.TransformDirection(calculatedPlayerMovement), out rayHit, .75f))
                    {
                        if (Vector3.Angle(rayHit.normal, _rigidbody.transform.up) > _maxSlopeAngle)
                        {
                            calculatedPlayerMovement.y = -_movementMultiplier;
                        }
                    }
#if UNITY_EDITOR
                    Debug.DrawRay(rayOrigin, _rigidbody.transform.TransformDirection(calculatedPlayerMovement), Color.green, 1);
#endif
                }
            }
            else
            {
                Quaternion slopeAngleRotation = Quaternion.FromToRotation(_rigidbody.transform.up, localGroundCheckHitNormal);
                calculatedPlayerMovement = slopeAngleRotation * calculatedPlayerMovement;

                float relativeSlopeAngle = Vector3.Angle(calculatedPlayerMovement, _rigidbody.transform.up) - 90f;
                calculatedPlayerMovement += calculatedPlayerMovement * (relativeSlopeAngle / 90f);

                if (groundSlopeAngle < _maxSlopeAngle)
                {
                    if (_input.moveIsPressed)
                    {
                        calculatedPlayerMovement.y += _gravityGrounded;
                    }
                }
                else
                {
                    float calculatedSlopeGravity = groundSlopeAngle * -.2f;
                    if (calculatedSlopeGravity < calculatedPlayerMovement.y)
                    {
                        calculatedPlayerMovement.y = calculatedSlopeGravity;
                    }
                }
            }
#if UNITY_EDITOR
            Debug.DrawRay(_rigidbody.position, _rigidbody.transform.TransformDirection(calculatedPlayerMovement), Color.red, .5f);
#endif
        }
        return calculatedPlayerMovement;
    }

    private Vector3 PlayerSprint()
    {
        Vector3 calculatePlayerSprint = _playerMoveInput;
        if (_input.moveIsPressed && _input.sprintIsPressed)
        {
            calculatePlayerSprint *= _sprintMultiplier;
        }
        return calculatePlayerSprint;
    }

    private float PlayerFallGravity()
    {
        float gravity = _playerMoveInput.y;
        if (_isPlayerGrounded)
        {
            _gravityFallCurrent = _gravityFallMin; // Reset
        }
        else
        {
            _playerFallTimer -= Time.fixedDeltaTime;
            if (_playerFallTimer < 0)
            {
                if (_playerFallTimer > _gravityFallMax)
                {
                    _gravityFallCurrent += _gravityFallIncrementAmount;
                }
                _playerFallTimer = _gravityFallIncrementTime;
            }
            gravity = _gravityFallCurrent;
        }
        return gravity;
    }

    private float PlayerJump()
    {
        float calculatedJumpInput = _playerMoveInput.y;

        SetJumpTimeCounter();
        SetCoyoteTimeCounter();
        SetJumpBufferTimeCounter();

        if (_jumpBufferTimeCounter > 0 && !_isPlayerJumping && _coyoteTimeCounter > 0)
        {
            if (Vector3.Angle(_rigidbody.transform.up, _groundCheckHit.normal) < _maxSlopeAngle)
            {
                calculatedJumpInput = _initialJumpForce;
                _isPlayerJumping = true;
                _jumpBufferTimeCounter = 0;
                _coyoteTimeCounter = 0;
            }
        }
        else if (_input.jumpIsPressed && _isPlayerJumping && !_isPlayerGrounded && _jumpTimeCounter > 0)
        {
            calculatedJumpInput = _initialJumpForce * _continualJumpForceMultiplier;
        }
        else if (_isPlayerJumping && _isPlayerGrounded)
        {
            _isPlayerJumping = false;
        }

        return calculatedJumpInput;
    }

    private void SetJumpTimeCounter()
    {
        if (_isPlayerJumping && !_isPlayerGrounded)
        {
            _jumpTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            _jumpTimeCounter = _jumpTime;
        }
    }

    private void SetCoyoteTimeCounter()
    {
        if (_isPlayerGrounded)
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    private void SetJumpBufferTimeCounter()
    {
        if (!_jumpWasPressedLastFrame && _input.jumpIsPressed)
        {
            _jumpBufferTimeCounter = _jumpBufferTime;
        }
        else if (_jumpBufferTimeCounter > 0)
        {
            _jumpBufferTimeCounter -= Time.fixedDeltaTime;
        }
        if (!_canHoldSpace)
        {
            _jumpWasPressedLastFrame = _input.jumpIsPressed;
        }
    }

    private void OnDrawGizmos()
    {
        if (_rigidbody)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector3(_rigidbody.position.x, (_rigidbody.position.y) - _capsuleColider.bounds.extents.y * _groundCheckDistance, _rigidbody.position.z) , _capsuleColider.radius * _groundCheckRadiusMultiplier);
        }
    }
}
