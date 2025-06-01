using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class HotspotController : MonoBehaviour
{
    [Header("UI References")]
    public GameObject contentPanel; 
    public Button hotspotButton; 
    public Button audioButton;   
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject imgPanel;     
    public Image infoImage;         // Image inside the ImgPanel
    public AudioSource audioSource;
    public ScrollRect scrollView;  
    public RectTransform contentRectTransform;

    [Header("Content")]
    public string hotspotTitle = "Information";
    [TextArea(3, 5)]
    public string description = "This is detailed information about this hotspot.";
    public Sprite infoSprite;
    public AudioClip infoAudio;

    [Header("Settings")]
    public bool hasText = true;
    public bool hasImage = false;
    public bool hasAudio = false;
    public bool autoPlayAudio = false;

    [Header("Layout Settings")]
    public float textPadding = 5f;      // Padding around text
    public float elementSpacing = 1f;   // Spacing between elements

    private bool isPanelOpen = false;

    void Start()
    {
        if (hotspotButton != null)
        {
            hotspotButton.onClick.AddListener(ToggleContentPanel);
        }

        
        if (audioButton != null)
        {
            audioButton.onClick.AddListener(ToggleAudio);
        }

        // Initially hide the panel
        if (contentPanel != null)
        {
            contentPanel.SetActive(false);
            isPanelOpen = false;
        }

        // Setup content based on configuration
        SetupContent();
    }

    public void ActivateHotspot()
    {
        ToggleContentPanel();
    }

    public void ToggleContentPanel()
    {
        if (contentPanel != null)
        {
            isPanelOpen = !isPanelOpen;
            contentPanel.SetActive(isPanelOpen);

            if (isPanelOpen)
            {
                PositionPanelTowardsUser();
                AdjustContentHeight();

                // Auto-play if requested
                if (hasAudio && autoPlayAudio && audioSource != null && infoAudio != null)
                {
                    //Debug.Log("autoplay audio");
                    audioSource.Play();
                }
            }
            else
            {
                // Stop audio when closing panel
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    public void CloseContentPanel()
    {
        if (contentPanel != null && isPanelOpen)
        {
            contentPanel.SetActive(false);
            isPanelOpen = false;

            // Stop audio when closing
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void ToggleAudio()
    {
        if (audioSource != null && hasAudio && infoAudio != null)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    void SetupContent()
    {
        // Configure text
        if (hasText)
        {
            if (titleText != null) titleText.text = hotspotTitle;
            if (descriptionText != null)
            {
                descriptionText.text = description;
                // Force text to update immediately for height calculation
                Canvas.ForceUpdateCanvases();
            }
        }
        else
        {
            if (titleText != null) titleText.gameObject.SetActive(false);
            if (descriptionText != null) descriptionText.gameObject.SetActive(false);
        }

        // Configure image and ImgPanel
        if (hasImage && imgPanel != null && infoImage != null && infoSprite != null)
        {
            imgPanel.SetActive(true);
            infoImage.sprite = infoSprite;
        }
        else if (imgPanel != null)
        {
            imgPanel.SetActive(false);
        }

        // Configure audio
        if (hasAudio && audioSource != null && infoAudio != null)
        {
            audioSource.clip = infoAudio;

            if (audioButton != null)
            {
                audioButton.gameObject.SetActive(true);
            }
        }
        else
        {
            if (audioButton != null)
            {
                audioButton.gameObject.SetActive(false);
            }
        }
    }

    void AdjustContentHeight()
    {
        if (contentRectTransform != null)
        {
            // Force canvas update to get accurate measurements
            Canvas.ForceUpdateCanvases();

            float totalHeight = 0f;

            //// Add title height
            //if (titleText != null && titleText.gameObject.activeInHierarchy && hasText)
            //{
            //    // Force text mesh pro to calculate preferred height
            //    titleText.ForceMeshUpdate();
            //    totalHeight += titleText.preferredHeight + elementSpacing;
            //}

            // Add description text height
            if (descriptionText != null && descriptionText.gameObject.activeInHierarchy && hasText)
            {
                // Force text mesh pro to calculate preferred height
                descriptionText.ForceMeshUpdate();
                totalHeight += descriptionText.preferredHeight;
            }

            // Add base padding
            totalHeight += textPadding * 2; // Top and bottom padding

            // Apply the new height to the content
            Vector2 newSize = contentRectTransform.sizeDelta;
            newSize.y = totalHeight;
            contentRectTransform.sizeDelta = newSize;

            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);

            // Reset scroll position to top
            if (scrollView != null)
            {
                Canvas.ForceUpdateCanvases();
                scrollView.verticalNormalizedPosition = 1f;
            }
        }
    }

    void PositionPanelTowardsUser()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null && contentPanel != null)
        {
            Vector3 directionToUser = (mainCamera.transform.position - contentPanel.transform.position).normalized;
            contentPanel.transform.rotation = Quaternion.LookRotation(-directionToUser);
        }
    }

    // Public method to update content at runtime
    public void UpdateContent(string newTitle, string newDescription, Sprite newImage = null, AudioClip newAudio = null)
    {
        hotspotTitle = newTitle;
        description = newDescription;

        if (newImage != null)
        {
            infoSprite = newImage;
            hasImage = true;
        }

        if (newAudio != null)
        {
            infoAudio = newAudio;
            hasAudio = true;
        }

        SetupContent();

        // If panel is currently open, adjust the height
        if (isPanelOpen)
        {
            AdjustContentHeight();
        }
    }

    public void OpenContentPanel()
    {
        if (contentPanel != null && !isPanelOpen)
        {
            ToggleContentPanel();
        }
    }
}