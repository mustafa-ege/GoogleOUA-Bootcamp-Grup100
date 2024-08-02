using UnityEngine;
using UnityEngine.UI;

public class ImageToggleManager : MonoBehaviour
{
    public Image[] images;  // Array to hold references to all images
    private int currentIndex = 0;  // Index of the currently active image

    public GameObject[] panel; // The panel GameObject to show/hide
    private bool isPanelActive = false;

    void Start()
    {
        // Ensure all panels are initially inactive
        if (panel != null)
        {
            foreach (GameObject p in panel)
            {
                p.SetActive(false);
            }
        }

        // Hide the cursor and lock it to the center of the screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check for Esc key input to toggle panel visibility and cursor visibility
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
            Cursor.visible = isPanelActive;

            if (isPanelActive)
            {
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor when the panel is active
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor when the panel is inactive
            }
        }

        // Hide cursor on click if the panel is not active
        if (!isPanelActive && Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // This method can be linked to a button's onClick event for "Next"
    public void ToggleNextImage()
    {
        // Deactivate the current image
        images[currentIndex].gameObject.SetActive(false);

        // Move to the next image, looping back to the start if necessary
        currentIndex = (currentIndex + 1) % images.Length;

        // Activate the new current image
        SetActiveImage(currentIndex);
    }

    // This method can be linked to a button's onClick event for "Previous"
    public void TogglePreviousImage()
    {
        // Deactivate the current image
        images[currentIndex].gameObject.SetActive(false);

        // Move to the previous image, looping back to the end if necessary
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;

        // Activate the new current image
        SetActiveImage(currentIndex);
    }

    // Set active only the image at the specified index, deactivating all others
    private void SetActiveImage(int index)
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == index);
        }
    }

    // Toggle the panel's visibility
    private void TogglePanel()
    {
        isPanelActive = !isPanelActive;

        if (panel != null)
        {
            foreach (GameObject p in panel)
            {
                p.SetActive(isPanelActive);
            }
        }
    }
}
