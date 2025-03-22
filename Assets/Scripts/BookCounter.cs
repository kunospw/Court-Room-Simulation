using TMPro;
using UnityEngine;

public class BookCounter : MonoBehaviour
{
    public int bookCount = 0;
    public TextMeshProUGUI counterText;

    public AudioSource audioSource; // Reference to AudioSource
    public AudioClip winMusic;      // The win music clip
    public AudioClip normalMusic;   // The normal background music

    private const int WIN_CONDITION = 15;

    void Start()
    {
        UpdateCounterText();

        // Ensure AudioSource is playing the normal background music
        if (audioSource != null && normalMusic != null)
        {
            audioSource.clip = normalMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            bookCount++;
            UpdateCounterText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            bookCount--;
            UpdateCounterText();
        }
    }

    void UpdateCounterText()
    {
        if (counterText != null)
        {
            if (bookCount >= WIN_CONDITION)
            {
                counterText.text = "You've Win!";

                // Play win music if it's set
                if (audioSource != null && winMusic != null)
                {
                    audioSource.clip = winMusic;
                    audioSource.loop = false; // Win music should play once
                    audioSource.Play();
                }
            }
            else
            {
                counterText.text = "Book Counter:\n" + bookCount;
            }
        }
    }
}
