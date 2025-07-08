/*using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Android;

public class EyeTrackingDebug : MonoBehaviour
{
    [Header("UI Debug Display")]
    public TextMeshProUGUI debugDisplay;
    //public TextMeshProUGUI linkStatusDisplay;

    [Header("Eye Tracking References")]
    public OVREyeGaze leftEyeGaze;
    public OVREyeGaze rightEyeGaze;

    private float updateInterval = 0.5f; // Update every 0.5 seconds

    void Start()
    {
        RequestEyeTrackingPermission();
        StartCoroutine(ContinuousEyeTrackingCheck());
    }

    void RequestEyeTrackingPermission()
    {
        if (!Permission.HasUserAuthorizedPermission("com.oculus.permission.EYE_TRACKING"))
        {
            Permission.RequestUserPermission("com.oculus.permission.EYE_TRACKING");
        }
    }

    IEnumerator ContinuousEyeTrackingCheck()
    {
        while (true)
        {
            UpdateLinkStatus();
            UpdateEyeTrackingStatus();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateLinkStatus()
    {
        string linkInfo = "=== QUEST LINK STATUS ===\n";

        // Check if running through Link
        bool isLink = Application.platform == RuntimePlatform.WindowsEditor ||
                     Application.platform == RuntimePlatform.WindowsPlayer;

        linkInfo += $"Platform: {Application.platform}\n";
        linkInfo += $"Is Quest Link: {isLink}\n";

        if (OVRManager.instance != null)
        {
            linkInfo += $"OVR Manager Present: True\n";
            linkInfo += $"HMD Present: {OVRManager.hasInputFocus}\n";
        }
        else
        {
            linkInfo += $"OVR Manager: NULL\n";
        }

        // Check OVR Plugin status
        linkInfo += $"OVR Plugin Eye Support: {OVRPlugin.eyeTrackingSupported}\n";

//#if UNITY_ANDROID && !UNITY_EDITOR
//        linkInfo += $"Has Eye Permission: {OVRPlugin.hasEyeTrackingPermission}\n";
//#else
//        linkInfo += "Eye Permission: N/A (Link/Editor)\n";
//#endif

//        if (linkStatusDisplay != null)
//            linkStatusDisplay.text = linkInfo;
    }

    void UpdateEyeTrackingStatus()
    {
        string eyeInfo = "=== EYE TRACKING STATUS ===\n";

        // Auto-find eye gazes if not assigned
        if (leftEyeGaze == null || rightEyeGaze == null)
        {
            FindEyeGazeComponents();
        }

        // Check OVR Plugin instead of OVRManager for eye tracking
        eyeInfo += $"OVRPlugin Eye Tracking: {OVRPlugin.eyeTrackingSupported}\n";

        // Left Eye Status
        if (leftEyeGaze != null)
        {
            eyeInfo += $"\nLEFT EYE:\n";
            eyeInfo += $"  Enabled: {leftEyeGaze.EyeTrackingEnabled}\n";
            eyeInfo += $"  Confidence: {leftEyeGaze.Confidence:F3}\n";
            eyeInfo += $"  Position: {leftEyeGaze.transform.position}\n";
            eyeInfo += $"  Rotation: {leftEyeGaze.transform.rotation.eulerAngles}\n";
        }
        else
        {
            eyeInfo += "\nLEFT EYE: NOT FOUND\n";
        }

        // Right Eye Status
        if (rightEyeGaze != null)
        {
            eyeInfo += $"\nRIGHT EYE:\n";
            eyeInfo += $"  Enabled: {rightEyeGaze.EyeTrackingEnabled}\n";
            eyeInfo += $"  Confidence: {rightEyeGaze.Confidence:F3}\n";
            eyeInfo += $"  Position: {rightEyeGaze.transform.position}\n";
            eyeInfo += $"  Rotation: {rightEyeGaze.transform.rotation.eulerAngles}\n";
        }
        else
        {
            eyeInfo += "\nRIGHT EYE: NOT FOUND\n";
        }

        // Overall status
        bool eyeTrackingWorking = (leftEyeGaze != null && leftEyeGaze.EyeTrackingEnabled) ||
                                 (rightEyeGaze != null && rightEyeGaze.EyeTrackingEnabled);

        eyeInfo += $"\n=== OVERALL STATUS ===\n";
        eyeInfo += $"Eye Tracking Working: {eyeTrackingWorking}\n";

        if (!eyeTrackingWorking)
        {
            eyeInfo += "\n=== TROUBLESHOOTING ===\n";
            eyeInfo += "• Check Quest Features Eye Tracking = Required\n";
            eyeInfo += "• Verify Quest Pro/3 (Quest 2 unsupported)\n";
            eyeInfo += "• Run eye calibration in Quest settings\n";
            eyeInfo += "• Consider building to device vs Link\n";
        }

        if (debugDisplay != null)
            debugDisplay.text = eyeInfo;

        // Console logging for key status changes
        Debug.Log($"Eye Tracking - Left: {leftEyeGaze?.EyeTrackingEnabled}, Right: {rightEyeGaze?.EyeTrackingEnabled}");
    }

    void FindEyeGazeComponents()
    {
        OVREyeGaze[] eyeGazes = FindObjectsOfType<OVREyeGaze>();

        foreach (OVREyeGaze eyeGaze in eyeGazes)
        {
            if (eyeGaze.Eye == OVREyeGaze.EyeId.Left && leftEyeGaze == null)
            {
                leftEyeGaze = eyeGaze;
                Debug.Log($"Auto-found Left Eye Gaze: {eyeGaze.name}");
            }
            else if (eyeGaze.Eye == OVREyeGaze.EyeId.Right && rightEyeGaze == null)
            {
                rightEyeGaze = eyeGaze;
                Debug.Log($"Auto-found Right Eye Gaze: {eyeGaze.name}");
            }
        }
    }

    // Force enable eye tracking - Updated for newer SDK
    [ContextMenu("Force Enable Eye Tracking")]
    public void ForceEnableEyeTracking()
    {
        Debug.Log("Eye tracking is controlled through Quest Features settings in OVRManager");
        Debug.Log("Check: Quest Features > Eye Tracking Support = 'Required'");

        // Try to request permissions
//#if UNITY_ANDROID && !UNITY_EDITOR
//        try
//        {
//            OVRPlugin.RequestEyeTrackingPermission();
//            Debug.Log("Requested eye tracking permission");
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"Permission request failed: {e.Message}");
//        }
//#endif
    }
}*/






using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Android;

public class EyeTrackingDebug : MonoBehaviour
{
    [Header("UI Debug Display")]
    public TextMeshProUGUI debugDisplay;
    public TextMeshProUGUI linkStatusDisplay;

    [Header("Eye Tracking References")]
    public OVREyeGaze leftEyeGaze;
    public OVREyeGaze rightEyeGaze;

    [Header("Visualization Settings")]
    public GameObject leftEyeSphere;
    public GameObject rightEyeSphere;
    public float sphereDistance = 2f; // Distance from eye to place sphere
    public float sphereSize = 0.1f; // Size of the visualization spheres
    public Material leftEyeMaterial;
    public Material rightEyeMaterial;

    private float updateInterval = 0.5f; // Update every 0.5 seconds

    void Start()
    {
        RequestEyeTrackingPermission();
        CreateVisualizationSpheres();
        StartCoroutine(ContinuousEyeTrackingCheck());
    }

    void CreateVisualizationSpheres()
    {
        // Create left eye sphere if not assigned
        if (leftEyeSphere == null)
        {
            leftEyeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            leftEyeSphere.name = "LeftEyeVisualization";
            leftEyeSphere.transform.localScale = Vector3.one * sphereSize;

            // Remove collider since we don't need it
            if (leftEyeSphere.GetComponent<Collider>())
                DestroyImmediate(leftEyeSphere.GetComponent<Collider>());

            // Apply material or set color
            if (leftEyeMaterial != null)
                leftEyeSphere.GetComponent<Renderer>().material = leftEyeMaterial;
            else
                leftEyeSphere.GetComponent<Renderer>().material.color = Color.red;
        }

        // Create right eye sphere if not assigned
        if (rightEyeSphere == null)
        {
            rightEyeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            rightEyeSphere.name = "RightEyeVisualization";
            rightEyeSphere.transform.localScale = Vector3.one * sphereSize;

            // Remove collider since we don't need it
            if (rightEyeSphere.GetComponent<Collider>())
                DestroyImmediate(rightEyeSphere.GetComponent<Collider>());

            // Apply material or set color
            if (rightEyeMaterial != null)
                rightEyeSphere.GetComponent<Renderer>().material = rightEyeMaterial;
            else
                rightEyeSphere.GetComponent<Renderer>().material.color = Color.blue;
        }

        Debug.Log("Eye visualization spheres created");
    }

    void Update()
    {
        UpdateEyeVisualization();
    }

    void UpdateEyeVisualization()
    {
        // Update left eye sphere position - ALWAYS show if eye gaze component exists
        if (leftEyeGaze != null && leftEyeSphere != null)
        {
            // Always position sphere based on eye gaze transform data
            Vector3 gazeDirection = leftEyeGaze.transform.forward;
            Vector3 spherePosition = leftEyeGaze.transform.position + (gazeDirection * sphereDistance);

            leftEyeSphere.transform.position = spherePosition;
            leftEyeSphere.transform.rotation = leftEyeGaze.transform.rotation;

            // Always make sphere visible to show raw transform data
            leftEyeSphere.SetActive(true);

            // Visual indicator of tracking state
            if (leftEyeGaze.EyeTrackingEnabled && leftEyeGaze.Confidence > 0.1f)
            {
                // Green tint when tracking is working
                leftEyeSphere.GetComponent<Renderer>().material.color = Color.green;
                float confidenceScale = Mathf.Lerp(0.5f, 1f, leftEyeGaze.Confidence);
                leftEyeSphere.transform.localScale = Vector3.one * sphereSize * confidenceScale;
            }
            else
            {
                // Red tint when tracking is not working but still show position
                leftEyeSphere.GetComponent<Renderer>().material.color = Color.red;
                leftEyeSphere.transform.localScale = Vector3.one * sphereSize;
            }
        }

        // Update right eye sphere position - ALWAYS show if eye gaze component exists
        if (rightEyeGaze != null && rightEyeSphere != null)
        {
            // Always position sphere based on eye gaze transform data
            Vector3 gazeDirection = rightEyeGaze.transform.forward;
            Vector3 spherePosition = rightEyeGaze.transform.position + (gazeDirection * sphereDistance);

            rightEyeSphere.transform.position = spherePosition;
            rightEyeSphere.transform.rotation = rightEyeGaze.transform.rotation;

            // Always make sphere visible to show raw transform data
            rightEyeSphere.SetActive(true);

            // Visual indicator of tracking state
            if (rightEyeGaze.EyeTrackingEnabled && rightEyeGaze.Confidence > 0.1f)
            {
                // Green tint when tracking is working
                rightEyeSphere.GetComponent<Renderer>().material.color = Color.green;
                float confidenceScale = Mathf.Lerp(0.5f, 1f, rightEyeGaze.Confidence);
                rightEyeSphere.transform.localScale = Vector3.one * sphereSize * confidenceScale;
            }
            else
            {
                // Blue tint when tracking is not working but still show position
                rightEyeSphere.GetComponent<Renderer>().material.color = Color.blue;
                rightEyeSphere.transform.localScale = Vector3.one * sphereSize;
            }
        }
    }

    void RequestEyeTrackingPermission()
    {
        if (!Permission.HasUserAuthorizedPermission("com.oculus.permission.EYE_TRACKING"))
        {
            Permission.RequestUserPermission("com.oculus.permission.EYE_TRACKING");
        }
    }

    IEnumerator ContinuousEyeTrackingCheck()
    {
        while (true)
        {
            UpdateLinkStatus();
            UpdateEyeTrackingStatus();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateLinkStatus()
    {
        string linkInfo = "=== QUEST LINK STATUS ===\n";

        // Check if running through Link
        bool isLink = Application.platform == RuntimePlatform.WindowsEditor ||
                     Application.platform == RuntimePlatform.WindowsPlayer;

        linkInfo += $"Platform: {Application.platform}\n";
        linkInfo += $"Is Quest Link: {isLink}\n";

        if (OVRManager.instance != null)
        {
            linkInfo += $"OVR Manager Present: True\n";
            linkInfo += $"HMD Present: {OVRManager.hasInputFocus}\n";
        }
        else
        {
            linkInfo += $"OVR Manager: NULL\n";
        }

        // Check OVR Plugin status
        linkInfo += $"OVR Plugin Eye Support: {OVRPlugin.eyeTrackingSupported}\n";

//#if UNITY_ANDROID && !UNITY_EDITOR
//                linkInfo += $"Has Eye Permission: {OVRPlugin.hasEyeTrackingPermission}\n";
//#else
//        linkInfo += "Eye Permission: N/A (Link/Editor)\n";
//#endif

//        if (linkStatusDisplay != null)
//            linkStatusDisplay.text = linkInfo;
    }

    void UpdateEyeTrackingStatus()
    {
        string eyeInfo = "=== EYE TRACKING STATUS ===\n";

        // Auto-find eye gazes if not assigned
        if (leftEyeGaze == null || rightEyeGaze == null)
        {
            FindEyeGazeComponents();
        }

        // Check OVR Plugin instead of OVRManager for eye tracking
        eyeInfo += $"OVRPlugin Eye Tracking: {OVRPlugin.eyeTrackingSupported}\n";

        // Left Eye Status
        if (leftEyeGaze != null)
        {
            eyeInfo += $"\nLEFT EYE:\n";
            eyeInfo += $"  Enabled: {leftEyeGaze.EyeTrackingEnabled}\n";
            eyeInfo += $"  Confidence: {leftEyeGaze.Confidence:F3}\n";
            eyeInfo += $"  Position: {leftEyeGaze.transform.position}\n";
            eyeInfo += $"  Rotation: {leftEyeGaze.transform.rotation.eulerAngles}\n";
            eyeInfo += $"  Sphere Position: {(leftEyeSphere != null ? leftEyeSphere.transform.position.ToString() : "N/A")}\n";
            eyeInfo += $"  Sphere Active: {(leftEyeSphere != null ? leftEyeSphere.activeInHierarchy.ToString() : "N/A")}\n";
        }
        else
        {
            eyeInfo += "\nLEFT EYE: NOT FOUND\n";
        }

        // Right Eye Status
        if (rightEyeGaze != null)
        {
            eyeInfo += $"\nRIGHT EYE:\n";
            eyeInfo += $"  Enabled: {rightEyeGaze.EyeTrackingEnabled}\n";
            eyeInfo += $"  Confidence: {rightEyeGaze.Confidence:F3}\n";
            eyeInfo += $"  Position: {rightEyeGaze.transform.position}\n";
            eyeInfo += $"  Rotation: {rightEyeGaze.transform.rotation.eulerAngles}\n";
            eyeInfo += $"  Sphere Position: {(rightEyeSphere != null ? rightEyeSphere.transform.position.ToString() : "N/A")}\n";
            eyeInfo += $"  Sphere Active: {(rightEyeSphere != null ? rightEyeSphere.activeInHierarchy.ToString() : "N/A")}\n";
        }
        else
        {
            eyeInfo += "\nRIGHT EYE: NOT FOUND\n";
        }

        // Overall status
        bool eyeTrackingWorking = (leftEyeGaze != null && leftEyeGaze.EyeTrackingEnabled) ||
                                 (rightEyeGaze != null && rightEyeGaze.EyeTrackingEnabled);

        eyeInfo += $"\n=== OVERALL STATUS ===\n";
        eyeInfo += $"Eye Tracking Working: {eyeTrackingWorking}\n";
        eyeInfo += $"Visualization Distance: {sphereDistance}m\n";
        eyeInfo += $"Sphere Size: {sphereSize}\n";

        if (!eyeTrackingWorking)
        {
            eyeInfo += "\n=== TROUBLESHOOTING ===\n";
            eyeInfo += "• Check Quest Features Eye Tracking = Required\n";
            eyeInfo += "• Verify Quest Pro/3 (Quest 2 unsupported)\n";
            eyeInfo += "• Run eye calibration in Quest settings\n";
            eyeInfo += "• Consider building to device vs Link\n";
        }

        if (debugDisplay != null)
            debugDisplay.text = eyeInfo;

        // Console logging for key status changes
        Debug.Log($"Eye Tracking - Left: {leftEyeGaze?.EyeTrackingEnabled}, Right: {rightEyeGaze?.EyeTrackingEnabled}");
    }

    void FindEyeGazeComponents()
    {
        OVREyeGaze[] eyeGazes = FindObjectsOfType<OVREyeGaze>();

        foreach (OVREyeGaze eyeGaze in eyeGazes)
        {
            if (eyeGaze.Eye == OVREyeGaze.EyeId.Left && leftEyeGaze == null)
            {
                leftEyeGaze = eyeGaze;
                Debug.Log($"Auto-found Left Eye Gaze: {eyeGaze.name}");
            }
            else if (eyeGaze.Eye == OVREyeGaze.EyeId.Right && rightEyeGaze == null)
            {
                rightEyeGaze = eyeGaze;
                Debug.Log($"Auto-found Right Eye Gaze: {eyeGaze.name}");
            }
        }
    }

    // Force enable eye tracking - Updated for newer SDK
    [ContextMenu("Force Enable Eye Tracking")]
    public void ForceEnableEyeTracking()
    {
        Debug.Log("Eye tracking is controlled through Quest Features settings in OVRManager");
        Debug.Log("Check: Quest Features > Eye Tracking Support = 'Required'");

        // Try to request permissions
//#if UNITY_ANDROID && !UNITY_EDITOR
//                try
//                {
//                    OVRPlugin.RequestEyeTrackingPermission();
//                    Debug.Log("Requested eye tracking permission");
//                }
//                catch (System.Exception e)
//                {
//                    Debug.LogError($"Permission request failed: {e.Message}");
//                }
//#endif
    }

    // Utility methods for runtime adjustment
    [ContextMenu("Increase Sphere Distance")]
    public void IncreaseSphereDistance()
    {
        sphereDistance += 0.5f;
        Debug.Log($"Sphere distance increased to: {sphereDistance}m");
    }

    [ContextMenu("Decrease Sphere Distance")]
    public void DecreaseSphereDistance()
    {
        sphereDistance = Mathf.Max(0.5f, sphereDistance - 0.5f);
        Debug.Log($"Sphere distance decreased to: {sphereDistance}m");
    }

    [ContextMenu("Toggle Sphere Visibility")]
    public void ToggleSphereVisibility()
    {
        if (leftEyeSphere != null)
            leftEyeSphere.SetActive(!leftEyeSphere.activeInHierarchy);
        if (rightEyeSphere != null)
            rightEyeSphere.SetActive(!rightEyeSphere.activeInHierarchy);
    }
}