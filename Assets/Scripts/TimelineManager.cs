using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class TimelineManager : MonoBehaviour
{
    [Header("Director & Chapters")]
    public PlayableDirector director;                           // The Timeline player
    public List<PlayableAsset> chapters = new List<PlayableAsset>(); // One asset per chapter

    [Header("UI")]
    public ChapterSelectDynamic chapterSelect;                  // Drag your ChapterSelectDynamic here

    private int currentIndex = 0;                               // 0-based index of current chapter

    void Start()
    {
        if (director == null || chapters.Count == 0 || chapterSelect == null)
        {
            Debug.LogError("TimelineManager: Assign director, chapters and chapterSelect!", this);
            enabled = false;
            return;
        }

        // When a timeline finishes, we'll auto-advance to the next one :contentReference[oaicite:0]{index=0}
        director.stopped += OnDirectorStopped;

        // Kick off chapter 1
        PlayChapter(0);
    }

    private void OnDirectorStopped(PlayableDirector pd)
    {
        // Auto-advance
        int next = currentIndex + 1;
        if (next < chapters.Count)
            PlayChapter(next);
    }

    /// <summary>
    /// Stops any playing timeline and plays the one at [index]. Also
    /// updates the dropdownÅfs active option via CurrentChapter setter.
    /// </summary>
    public void PlayChapter(int index)
    {
        if (index < 0 || index >= chapters.Count) return;

        currentIndex = index;

        // Swap the asset :contentReference[oaicite:1]{index=1}
        director.playableAsset = chapters[index];

        // Update the UI (1-based) via your property :contentReference[oaicite:2]{index=2}
        chapterSelect.CurrentChapter = index + 1;

        // Restart from the beginning and play :contentReference[oaicite:3]{index=3}
        director.time = 0;
        director.Play();
    }
}
