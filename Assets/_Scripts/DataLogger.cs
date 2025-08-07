using UnityEngine;
using System.IO;
using System.Text;

public class DataLogger : MonoBehaviour
{
    [Header("Logging Settings")]
    public bool enableLogging = true;
    public float loggingRate = 30f; // Hz - how many times per second to log
    public string fileName = "eye_tracking_data.txt";

    [Header("Eye Tracking References")]
    public OVREyeGaze leftEyeGaze;
    public OVREyeGaze rightEyeGaze;

    private string filePath;
    private StreamWriter writer;
    private float lastLogTime;
    private float logInterval;

    void Start()
    {
        // Set up file path - this will save to the Quest's internal storage
        filePath = Path.Combine(Application.persistentDataPath, fileName);
        logInterval = 1f / loggingRate;

        // Auto-find eye tracking components if not assigned
        FindEyeGazeComponents();

        // Start logging
        if (enableLogging)
        {
            StartLogging();
        }

        Debug.Log($"Eye tracking data will be saved to: {filePath}");
    }

    void StartLogging()
    {
        try
        {
            writer = new StreamWriter(filePath, false); // false = overwrite existing file

            // Write header
            writer.WriteLine("Timestamp,LeftEyeEnabled,LeftEyeConfidence,LeftEyePosX,LeftEyePosY,LeftEyePosZ,LeftEyeRotX,LeftEyeRotY,LeftEyeRotZ,RightEyeEnabled,RightEyeConfidence,RightEyePosX,RightEyePosY,RightEyePosZ,RightEyeRotX,RightEyeRotY,RightEyeRotZ");

            Debug.Log("Eye tracking logging started");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to start logging: {e.Message}");
            enableLogging = false;
        }
    }

    void Update()
    {
        if (enableLogging && writer != null && Time.time - lastLogTime >= logInterval)
        {
            LogEyeData();
            lastLogTime = Time.time;
        }
    }

    void LogEyeData()
    {
        StringBuilder logLine = new StringBuilder();

        // Timestamp
        logLine.Append(Time.time.ToString("F3"));
        logLine.Append(",");

        // Left eye data
        if (leftEyeGaze != null)
        {
            logLine.Append(leftEyeGaze.EyeTrackingEnabled ? "1" : "0");
            logLine.Append(",");
            logLine.Append(leftEyeGaze.Confidence.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.position.x.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.position.y.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.position.z.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.rotation.eulerAngles.x.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.rotation.eulerAngles.y.ToString("F3"));
            logLine.Append(",");
            logLine.Append(leftEyeGaze.transform.rotation.eulerAngles.z.ToString("F3"));
            logLine.Append(",");
        }
        else
        {
            // Empty left eye data
            logLine.Append("0,0,0,0,0,0,0,0,");
        }

        // Right eye data
        if (rightEyeGaze != null)
        {
            logLine.Append(rightEyeGaze.EyeTrackingEnabled ? "1" : "0");
            logLine.Append(",");
            logLine.Append(rightEyeGaze.Confidence.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.position.x.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.position.y.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.position.z.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.rotation.eulerAngles.x.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.rotation.eulerAngles.y.ToString("F3"));
            logLine.Append(",");
            logLine.Append(rightEyeGaze.transform.rotation.eulerAngles.z.ToString("F3"));
        }
        else
        {
            // Empty right eye data
            logLine.Append("0,0,0,0,0,0,0,0");
        }

        // Write to file
        writer.WriteLine(logLine.ToString());
        writer.Flush(); // Ensure data is written immediately
    }

    void FindEyeGazeComponents()
    {
        OVREyeGaze[] eyeGazes = FindObjectsOfType<OVREyeGaze>();

        foreach (OVREyeGaze eyeGaze in eyeGazes)
        {
            if (eyeGaze.Eye == OVREyeGaze.EyeId.Left && leftEyeGaze == null)
            {
                leftEyeGaze = eyeGaze;
                Debug.Log("Found left eye gaze component");
            }
            else if (eyeGaze.Eye == OVREyeGaze.EyeId.Right && rightEyeGaze == null)
            {
                rightEyeGaze = eyeGaze;
                Debug.Log("Found right eye gaze component");
            }
        }
    }

    [ContextMenu("Start Logging")]
    public void StartLoggingManually()
    {
        if (!enableLogging)
        {
            enableLogging = true;
            StartLogging();
        }
    }

    [ContextMenu("Stop Logging")]
    public void StopLogging()
    {
        if (writer != null)
        {
            writer.Close();
            writer = null;
            Debug.Log($"Logging stopped. File saved at: {filePath}");
        }
        enableLogging = false;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && writer != null)
        {
            writer.Flush(); // Save data when app is paused
        }
    }

    void OnDestroy()
    {
        StopLogging();
    }

    void OnApplicationQuit()
    {
        StopLogging();
    }
}