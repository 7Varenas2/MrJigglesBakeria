using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonDoorRaycast2 : MonoBehaviour
{
    [SerializeField] private int rayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string excludeLayerName = null;
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private RawImage crosshair = null;
    [SerializeField] private Spawner spawner = null;

    Camera cam;

    private bool isCrosshairActive;
    private bool doOnce;

    private const string interactableTag = "DoorButton";

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

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if (!doOnce)
                {
                    CrosshairChange(true);
                }

                isCrosshairActive = true;
                doOnce = true;
            }
        }

        else
        {
            if(isCrosshairActive)
            {
                CrosshairChange(false);
                doOnce = false;
            }
        }
    }

    void CrosshairChange(bool on)
    {
        if(on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }

    void MousePressed(InputAction.CallbackContext context)
    {
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
 