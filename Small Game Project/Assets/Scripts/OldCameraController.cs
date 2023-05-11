using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OldCameraController : MonoBehaviour
{
    [SerializeField] private GameObject _followObject;

    private Vector3 _focusPoint;
    private Vector3 _focusPointVelocity;
    private Vector3 _rotationVelocity;
    private float _radiusVelocity;
    private Vector3 _targetForward;


    [SerializeField] private Vector3 _focusPointOffset;
    [SerializeField] private Vector3 _focusPointSmoothTime;


    [SerializeField] private float _pitchAngle;
    [SerializeField] private Vector3 _rotationSmoothTime;


    [SerializeField] private float _targetRadius;
    [SerializeField] private float _radiusSmoothTime;


    [SerializeField] private float _manualCameraDuration;
    public float manualCameraTimer { get; private set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        manualCameraTimer -= Time.deltaTime;
        if (manualCameraTimer < 0.0f)
        {
            _targetForward = Quaternion.AngleAxis(_pitchAngle, _followObject.transform.right) * _followObject.transform.forward;
        }

        Vector3 targetFocusPoint = _followObject.transform.position + _followObject.transform.TransformVector(_focusPointOffset);
        _focusPoint = new Vector3(Mathf.SmoothDamp(_focusPoint.x, targetFocusPoint.x, ref _focusPointVelocity.x, _focusPointSmoothTime.x),
            Mathf.SmoothDamp(_focusPoint.y, targetFocusPoint.y, ref _focusPointVelocity.y, _focusPointSmoothTime.y),
            Mathf.SmoothDamp(_focusPoint.z, targetFocusPoint.z, ref _focusPointVelocity.z, _focusPointSmoothTime.z));

        Vector3 targetEuler = Quaternion.LookRotation(_targetForward, _followObject.transform.up).eulerAngles;
        Vector3 currentEuler = transform.rotation.eulerAngles;

        transform.rotation = Quaternion.Euler(Mathf.SmoothDampAngle(currentEuler.x, targetEuler.x, ref _rotationVelocity.x, _rotationSmoothTime.x),
            Mathf.SmoothDampAngle(currentEuler.y, targetEuler.y, ref _rotationVelocity.y, _rotationSmoothTime.y),
            Mathf.SmoothDampAngle(currentEuler.z, targetEuler.z, ref _rotationVelocity.z, _rotationSmoothTime.z));

        float currentRadius = Vector3.Distance(_focusPoint, transform.position);
        float radius = Mathf.SmoothDamp(currentRadius, _targetRadius, ref _radiusVelocity, _radiusSmoothTime);

        transform.position = _focusPoint - radius * transform.forward;
    }

    #region Input
    public void ManuallySetTargetForward(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>().normalized;
        if (manualCameraTimer < 0.0f)
        {
            _targetForward = transform.forward;
        }

        manualCameraTimer = _manualCameraDuration;


        _targetForward = Quaternion.AngleAxis(-v.y, transform.right) * Quaternion.AngleAxis(v.x, _followObject.transform.up) * _targetForward;
    }
    #endregion

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_followObject.transform.position, _followObject.transform.position + _followObject.transform.TransformVector(_focusPointOffset));
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);


        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_followObject.transform.position + _followObject.transform.TransformVector(_focusPointOffset), _targetRadius);

    }

}
