using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonRaycast : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private Spawner spawner = null;

    Camera cam;

    private const string interactableTag = "Button";

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    void MousePressed(InputAction.CallbackContext context)
    {
        //  Debug.Log("Click");
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(ray, out hit, rayLength, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Click");
                spawner.Spawn();
            }
        }
    }
}