using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    // Making sure we are clicking the mouse button
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private float mouseDragPhysicsSpeed = 10;
    [SerializeField] private float mouseDragSpeed = 0.1f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;

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

    // Context helps get the coordinates of the mouse
    private void MousePressed(InputAction.CallbackContext context)
    {
        // Get perspective based off camera and mouse position
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null ) // Make sure we are hitting a collider
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

      
    }

    private IEnumerator DragUpdate(GameObject go)
    {
        if (go.tag != "Environment")
        {
            float initialDistance = Vector3.Distance(go.transform.position, mainCamera.transform.position);
            go.TryGetComponent<Rigidbody>(out var rb);
            while (mouseClick.ReadValue<float>() != 0)
            {
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (rb != null)
                {
                    Vector3 direction = ray.GetPoint(initialDistance) - go.transform.position;
                    rb.velocity = direction * mouseDragPhysicsSpeed;
                    yield return waitForFixedUpdate;
                }
                else
                {
                    go.transform.position = Vector3.SmoothDamp(go.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                    yield return null;
                }

            }
        }
        

    }

}
