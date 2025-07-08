using UnityEngine;

public class EyeGazeInteractor : MonoBehaviour
{
    [Header("Line Renderer Settings")]
    public LineRenderer lineRenderer;
    public float rayDistance = 10f;
    public LayerMask interactableLayer = -1;

    [Header("Eye Tracking")]
    public OVREyeGaze eyeGaze;

    [Header("Interaction")]
    public GameObject currentTarget;
    public float dwellTime = 2f;
    private float currentDwellTime = 0f;

    void Start()
    {
        // Get components if not assigned
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        if (eyeGaze == null)
            eyeGaze = GetComponent<OVREyeGaze>();

        // Configure line renderer
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        UpdateEyeGazeRay();
        HandleEyeGazeInteraction();
    }

    void UpdateEyeGazeRay()
    {
        if (eyeGaze != null && eyeGaze.EyeTrackingEnabled)
        {
            // Get eye gaze direction
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = eyeGaze.transform.forward;

            // Set line renderer positions
            lineRenderer.SetPosition(0, rayOrigin);
            lineRenderer.SetPosition(1, rayOrigin + rayDirection * rayDistance);

            // Enable line renderer
            lineRenderer.enabled = true;
        }
        else
        {
            // Disable line renderer if eye tracking is not available
            lineRenderer.enabled = false;
        }
    }

    void HandleEyeGazeInteraction()
    {
        if (eyeGaze != null && eyeGaze.EyeTrackingEnabled)
        {
            Vector3 rayOrigin = transform.position;
            Vector3 rayDirection = eyeGaze.transform.forward;

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayDistance, interactableLayer))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (currentTarget != hitObject)
                {
                    // New target
                    if (currentTarget != null)
                        OnGazeExit(currentTarget);

                    currentTarget = hitObject;
                    currentDwellTime = 0f;
                    OnGazeEnter(currentTarget);
                }
                else
                {
                    // Same target - increment dwell time
                    currentDwellTime += Time.deltaTime;

                    if (currentDwellTime >= dwellTime)
                    {
                        OnGazeSelect(currentTarget);
                        currentDwellTime = 0f; // Reset to prevent multiple selections
                    }
                }

                // Update line renderer endpoint to hit point
                lineRenderer.SetPosition(1, hit.point);
            }
            else
            {
                // No hit
                if (currentTarget != null)
                {
                    OnGazeExit(currentTarget);
                    currentTarget = null;
                    currentDwellTime = 0f;
                }

                // Reset line renderer to full distance
                lineRenderer.SetPosition(1, rayOrigin + rayDirection * rayDistance);
            }
        }
    }

    void OnGazeEnter(GameObject target)
    {
        Debug.Log("Gaze entered: " + target.name);
        // Add visual feedback here (e.g., highlight object)

        // Try to get EyeGazeTarget component
        EyeGazeTarget gazeTarget = target.GetComponent<EyeGazeTarget>();
        if (gazeTarget != null)
            gazeTarget.OnGazeEnter();
    }

    void OnGazeExit(GameObject target)
    {
        Debug.Log("Gaze exited: " + target.name);
        // Remove visual feedback here

        // Try to get EyeGazeTarget component
        EyeGazeTarget gazeTarget = target.GetComponent<EyeGazeTarget>();
        if (gazeTarget != null)
            gazeTarget.OnGazeExit();
    }

    void OnGazeSelect(GameObject target)
    {
        Debug.Log("Gaze selected: " + target.name);

        // Try to get EyeGazeTarget component
        EyeGazeTarget gazeTarget = target.GetComponent<EyeGazeTarget>();
        if (gazeTarget != null)
            gazeTarget.OnGazeSelect();
    }
}