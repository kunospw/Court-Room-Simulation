using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class ChapterSelectDynamic : MonoBehaviour
{
    private TMP_Dropdown chapterDropdown;

    [Header("Initial Settings")]
    [Tooltip("Which chapter to select on Start (1-based).")]
    [SerializeField, Min(1)]
    private int startingChapter = 1;    // serialized via [SerializeField] :contentReference[oaicite:4]{index=4}

    private int _currentChapter;
    private bool isStartingChapterOverridden = false;

    // Expose this so TimelineManager can call it
    public int CurrentChapter
    {
        get => _currentChapter;
        set
        {
            if (chapterDropdown == null)
            {
                _currentChapter = value; // before Awake
                return;
            }

            int optionCount = chapterDropdown.options.Count;
            _currentChapter = Mathf.Clamp(value, 1, optionCount);

            // Drop-down is zero-based under the hood :contentReference[oaicite:5]{index=5}
            chapterDropdown.value = _currentChapter - 1;
            chapterDropdown.RefreshShownValue();                    // redraw label :contentReference[oaicite:6]{index=6}

            // Override startingChapter only once, if you care about preserving editor-set defaults
            if (!isStartingChapterOverridden)
            {
                startingChapter = _currentChapter;
                isStartingChapterOverridden = true;
            }
        }
    }

    void Awake()
    {
        chapterDropdown = GetComponent<TMP_Dropdown>();

        // If the user picks a chapter, jump there :contentReference[oaicite:7]{index=7}
        chapterDropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void Start()
    {
        // Show whatever was set in the Inspector at start
        CurrentChapter = startingChapter;
    }

    private void OnDropdownChanged(int zeroBasedIndex)
    {
        // Broadcast back to TimelineManager (if assigned)
        var tm = FindObjectOfType<TimelineManager>();
        if (tm != null)
            tm.PlayChapter(zeroBasedIndex);
    }
}
