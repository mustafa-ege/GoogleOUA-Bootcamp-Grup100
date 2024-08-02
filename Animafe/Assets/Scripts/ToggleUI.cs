using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour
{
    public Image[] images;  // Array to hold references to all images
    private int currentIndex = 0;  // Index of the currently active image

    void Start()
    {
        // Ensure only the first image is active at the start
        SetActiveImage(currentIndex);
    }

    // This method can be linked to a button's onClick event
    public void ToggleNextImage()
    {
        // Deactivate the current image
        images[currentIndex].gameObject.SetActive(false);

        // Move to the next image, looping back to the start if necessary
        currentIndex = (currentIndex + 1) % images.Length;

        // Activate the new current image
        SetActiveImage(currentIndex);
    }
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
}
