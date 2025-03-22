using UnityEngine;

public class MenuInteractionToggle : MonoBehaviour
{
    public GameObject directInteractorHolder;
    public GameObject rayInteractorHolder;
    public GameObject menuPanel;

    // Call this method to toggle the menu state.
    public void ToggleMenu(bool isOpen)
    {
        // Toggle the menu UI.
        menuPanel.SetActive(isOpen);

        // When the menu is open, use the ray interactor; otherwise, use the direct interactor.
        if (rayInteractorHolder != null && directInteractorHolder != null)
        {
            rayInteractorHolder.SetActive(isOpen);
            directInteractorHolder.SetActive(!isOpen);
        }
    }
}
