using UnityEngine;

public class EyeGazeCursor : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Transform cursorSphere;
    public float cursorDistance = 2f;
    public Camera centerEyeCamera;

    [Header("Eye Tracking Objects")]
    public Transform leftEyeAnchor;
    public Transform rightEyeAnchor;

    void Start()
    {
        // Find eye anchors if not assigned
        if (leftEyeAnchor == null)
            leftEyeAnchor = GameObject.Find("LeftEyeAnchor")?.transform;
        if (rightEyeAnchor == null)
            rightEyeAnchor = GameObject.Find("RightEyeAnchor")?.transform;
        if (centerEyeCamera == null)
            centerEyeCamera = Camera.main;
    }

    void Update()
    {
        if (cursorSphere == null || centerEyeCamera == null) return;

        Vector3 gazeDirection = centerEyeCamera.transform.forward;
        Vector3 gazeOrigin = centerEyeCamera.transform.position;

        // If eye tracking is available, use combined eye direction
        if (leftEyeAnchor != null && rightEyeAnchor != null)
        {
            Vector3 combinedDirection = (leftEyeAnchor.forward + rightEyeAnchor.forward) * 0.5f;
            gazeDirection = combinedDirection.normalized;
        }

        // Position cursor
        Vector3 cursorPosition = gazeOrigin + gazeDirection * cursorDistance;
        cursorSphere.position = cursorPosition;

        // Optional: Make cursor face the camera
        cursorSphere.LookAt(centerEyeCamera.transform);
    }
}