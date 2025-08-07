using UnityEngine;

public class EyeGazeTest : MonoBehaviour
{
    [Header("Eye Gaze Components")]
    public OVREyeGaze leftEyeGaze;
    public OVREyeGaze rightEyeGaze;

    [Header("Debug Settings")]
    public bool showDebugLogs = true;
    public float logInterval = 0.1f; // Log every 0.1 seconds

    private float lastLogTime;

    void Start()
    {
        // Check if eye tracking is supported
        if (OVRPlugin.eyeTrackingEnabled)
        {
            Debug.Log("Eye tracking is enabled!");
        }
        else
        {
            Debug.LogWarning("Eye tracking is not enabled on this device.");
        }

        // Find eye gaze components if not assigned
        if (leftEyeGaze == null || rightEyeGaze == null)
        {
            FindEyeGazeComponents();
        }
    }

    void Update()
    {
        if (!showDebugLogs) return;

        // Log data at specified intervals
        if (Time.time - lastLogTime > logInterval)
        {
            LogEyeGazeData();
            lastLogTime = Time.time;
        }
    }

    void FindEyeGazeComponents()
    {
        OVREyeGaze[] eyeGazes = FindObjectsOfType<OVREyeGaze>();

        foreach (var eyeGaze in eyeGazes)
        {
            if (eyeGaze.Eye == OVREyeGaze.EyeId.Left)
            {
                leftEyeGaze = eyeGaze;
                Debug.Log("Found left eye gaze component");
            }
            else if (eyeGaze.Eye == OVREyeGaze.EyeId.Right)
            {
                rightEyeGaze = eyeGaze;
                Debug.Log("Found right eye gaze component");
            }
        }
    }

    void LogEyeGazeData()
    {
        // Check if components are valid
        if (leftEyeGaze == null || rightEyeGaze == null)
        {
            Debug.LogWarning("Eye gaze components not found!");
            return;
        }

        // Get eye tracking confidence
        bool leftConfident = leftEyeGaze.Confidence > 0.0f;
        bool rightConfident = rightEyeGaze.Confidence > 0.0f;

        // Log basic eye gaze data
        Debug.Log($"=== Eye Gaze Data ===");
        Debug.Log($"Left Eye - Confidence: {leftEyeGaze.Confidence:F2}, Confident: {leftConfident}");
        Debug.Log($"Right Eye - Confidence: {rightEyeGaze.Confidence:F2}, Confident: {rightConfident}");

        if (leftConfident)
        {
            Vector3 leftDirection = leftEyeGaze.transform.forward;
            Debug.Log($"Left Eye Direction: {leftDirection}");
        }

        if (rightConfident)
        {
            Vector3 rightDirection = rightEyeGaze.transform.forward;
            Debug.Log($"Right Eye Direction: {rightDirection}");
        }

        Debug.Log($"==================");
    }
}