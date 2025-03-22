#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;
    public Transform head;
    public float spawnDistance = 2f;

    // Arrays for interactors on both controllers.
    // For example, element 0 = Left Controller's interactors, element 1 = Right Controller's interactors.
    public GameObject[] directInteractorHolders;
    public GameObject[] rayInteractorHolders;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            bool menuState = !menu.activeSelf;
            menu.SetActive(menuState);

            // Position the menu in front of the head.
            Vector3 forward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            menu.transform.position = head.position + forward * spawnDistance;

            // Toggle both sets of interactors:
            // When the menu is open, enable ray interactors for UI interaction and disable direct interactors.
            foreach (GameObject rayInteractor in rayInteractorHolders)
            {
                if (rayInteractor != null)
                    rayInteractor.SetActive(menuState);
            }
            foreach (GameObject directInteractor in directInteractorHolders)
            {
                if (directInteractor != null)
                    directInteractor.SetActive(!menuState);
            }
        }

        // Update the menu orientation so it faces the user.
        if (menu.activeSelf)
        {
            menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
            menu.transform.forward *= -1; // Flip the menu.
        }
    }

    // Call this method (e.g., from a UI button) to quit the game.
    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
