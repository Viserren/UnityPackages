using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public bool usingOrbitalCamera { get; private set; }

    [SerializeField] HumanoidLandInput _input;
    CinemachineVirtualCamera _activeCamera;
    int _activeCameraPriorityModifier = 3000;
    [SerializeField] float _cameraZoomModifier = 32f;

    float _minCameraZoomDistance = 1;
    float _minOrbitCameraZoomDistance = 1;
    float _maxCameraZoomDistance = 30;
    float _maxOrbitCameraZoomDistance = 40;


    public Camera mainCamera;
    public CinemachineVirtualCamera cinemachine1stPerson;
    public CinemachineVirtualCamera cinemachine3rdPerson;
    CinemachineFramingTransposer _cinemachineFramingTransposer3rdPerson;
    public CinemachineVirtualCamera cinemachineOrbit;
    CinemachineFramingTransposer _cinemachineFramingTransposerOrbit;

    private void Awake()
    {
        _cinemachineFramingTransposer3rdPerson = cinemachine3rdPerson.GetCinemachineComponent<CinemachineFramingTransposer>();
        _cinemachineFramingTransposerOrbit = cinemachineOrbit.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        ChangeCamera(); // First time through, sets the default camera
    }

    private void Update()
    {
        if (!(_input.zoomCameraInput == 0))
        {
            ZoomCamera();
        }
        if (_input.changeCameraWasPressedThisFrame)
        {
            ChangeCamera();
        }
    }

    private void ZoomCamera()
    {
        if (_activeCamera == cinemachine3rdPerson)
        {
            _cinemachineFramingTransposer3rdPerson.m_CameraDistance = Mathf.Clamp(_cinemachineFramingTransposer3rdPerson.m_CameraDistance + (_input.invertScroll ? -_input.zoomCameraInput : _input.zoomCameraInput) / -_cameraZoomModifier, _minCameraZoomDistance, _maxCameraZoomDistance);
        }
        else if (_activeCamera == cinemachineOrbit)
        {
            _cinemachineFramingTransposerOrbit.m_CameraDistance = Mathf.Clamp(_cinemachineFramingTransposerOrbit.m_CameraDistance + (_input.invertScroll ? -_input.zoomCameraInput : _input.zoomCameraInput) / -_cameraZoomModifier, _minOrbitCameraZoomDistance, _maxOrbitCameraZoomDistance);
        }
    }

    private void ChangeCamera()
    {
        if (cinemachine3rdPerson == _activeCamera)
        {
            SetCameraPriorties(cinemachine3rdPerson, cinemachine1stPerson);
            usingOrbitalCamera = false;
            //mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("Player Self"));
        }
        else if (cinemachine1stPerson == _activeCamera)
        {
            SetCameraPriorties(cinemachine1stPerson, cinemachineOrbit);
            usingOrbitalCamera = true;
            //mainCamera.cullingMask |= 1 << LayerMask.NameToLayer("Player Self");
        }
        else if (cinemachineOrbit == _activeCamera)
        {
            SetCameraPriorties(cinemachineOrbit, cinemachine3rdPerson);
            _activeCamera = cinemachine3rdPerson;
            usingOrbitalCamera = false;
        }
        else
        {
            cinemachine3rdPerson.Priority += _activeCameraPriorityModifier;
            _activeCamera = cinemachine3rdPerson;
        }
    }

    private void SetCameraPriorties(CinemachineVirtualCamera currentCameraMode, CinemachineVirtualCamera newCameraMode)
    {
        currentCameraMode.Priority -= _activeCameraPriorityModifier;
        newCameraMode.Priority += _activeCameraPriorityModifier;
        _activeCamera = newCameraMode;
    }
}
