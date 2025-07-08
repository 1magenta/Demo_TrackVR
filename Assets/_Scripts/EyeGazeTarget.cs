using UnityEngine;
using UnityEngine.Events;

public class EyeGazeTarget : MonoBehaviour
{
    [Header("Visual Feedback")]
    public Material originalMaterial;
    public Material highlightMaterial;
    private Renderer objectRenderer;

    [Header("Events")]
    public UnityEvent OnGazeEnterEvent;
    public UnityEvent OnGazeExitEvent;
    public UnityEvent OnGazeSelectEvent;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
            originalMaterial = objectRenderer.material;
    }

    public void OnGazeEnter()
    {
        // Visual feedback
        if (objectRenderer != null && highlightMaterial != null)
            objectRenderer.material = highlightMaterial;

        OnGazeEnterEvent?.Invoke();
    }

    public void OnGazeExit()
    {
        // Reset visual feedback
        if (objectRenderer != null && originalMaterial != null)
            objectRenderer.material = originalMaterial;

        OnGazeExitEvent?.Invoke();
    }

    public void OnGazeSelect()
    {
        Debug.Log($"Selected: {gameObject.name}");
        OnGazeSelectEvent?.Invoke();
    }
}