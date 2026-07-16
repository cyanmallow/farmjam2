using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastCursorController : MonoBehaviour
{
    private InteractiveCursorControl controls;
    //[SerializeField] private InteractableManager interactableManager;
    [SerializeField] Texture2D interactiveCursorTexture;

    private readonly Cursor interactiveCursor;
    [SerializeField] private Transform newSelectionTransform;
    [SerializeField] private Transform currentSelectionTransform;

    public static Action MakeCursorDefault;
    public static Action MakeCursorInteractive;
    private bool isCursorInteractive = false;

    private void Awake()
    {
        controls = new InteractiveCursorControl();
        controls.Mouse.Click.performed += ctx => StartedClick();
        controls.Mouse.Click.canceled += ctx => EndedClick();
        MakeCursorDefault += DefaultCursorTexture;
        MakeCursorInteractive += InteractiveCursorTexture;
    }

    private void EndedClick()
    {
        OnClickInteractable();
    }

    private void OnClickInteractable()
    {
        if (newSelectionTransform != null)
        {
            IInteractable interactable =
                newSelectionTransform.gameObject.GetComponent<IInteractable>();
            interactable?.OnClickAction();
            newSelectionTransform = null;
        }
    }

    private void StartedClick()
    {
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        FindInteractable();
    }

    private void FindInteractable()
    {
        newSelectionTransform = null;
        // change below to raycast from the mouse position to the world
        // and check if it hits an interactable
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                newSelectionTransform = hit.transform;
                if (!isCursorInteractive)
                {
                    InteractiveCursorTexture();
                }
            }
        }

        if (currentSelectionTransform != newSelectionTransform)
        {
            currentSelectionTransform = newSelectionTransform;
            if (currentSelectionTransform == null)
            {
                DefaultCursorTexture();
            }
        }
    }

    private void InteractiveCursorTexture()
    {
        isCursorInteractive = true;
        // z doesnt matter since the cursor is a 2D texture
        Vector2 hotspot = new Vector2(interactiveCursorTexture.width / 2, 0);
        Cursor.SetCursor(interactiveCursorTexture, hotspot, CursorMode.Auto);
    }

    private void DefaultCursorTexture()
    {
        isCursorInteractive = false;
        Cursor.SetCursor(null, default, default);
    }
}
