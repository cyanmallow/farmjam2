using UnityEngine;
using UnityEngine.UIElements;

public class FaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform;
    private UIDocument uIDocument;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        uIDocument = GetComponent<UIDocument>();
    }

    void Update()
    {
        if (mainCameraTransform != null)
        {
            // Makes the GameObject face the camera directly
            uIDocument.transform.LookAt(uIDocument.transform.position + mainCameraTransform.rotation * Vector3.forward,
                             mainCameraTransform.rotation * Vector3.up);
        }
    }
}