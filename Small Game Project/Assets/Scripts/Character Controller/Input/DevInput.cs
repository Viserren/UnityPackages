using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevInput : MonoBehaviour
{
    [SerializeField] HumanoidLandInput _input;
    [SerializeField] GameObject devTools;

    private void Update()
    {
        if (_input.devToolsPressed)
        {
            ShowDevTools();
        }
    }

    void ShowDevTools()
    {
        if (!devTools.activeInHierarchy)
        {
            devTools.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            devTools.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
