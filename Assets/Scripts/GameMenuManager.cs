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

    public GameObject[] directInteractorHolders;
    public GameObject[] rayInteractorHolders;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            bool menuState = !menu.activeSelf;
            menu.SetActive(menuState);

            // Position & face the menu as beforeÅc
            Vector3 forward = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            menu.transform.position = head.position + forward * spawnDistance;

            foreach (var ray in rayInteractorHolders) ray?.SetActive(menuState);
            foreach (var direct in directInteractorHolders) direct?.SetActive(!menuState);
        }

        if (menu.activeSelf)
        {
            menu.transform.LookAt(
              new Vector3(head.position.x, menu.transform.position.y, head.position.z)
            );
            menu.transform.forward *= -1;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
