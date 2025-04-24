using UnityEngine;

public class ChapterSelectTester : MonoBehaviour
{
    [Tooltip("Which chapter to test selecting on Start (1‑based).")]
    public int testChapter = 5;

    void Start()
    {
        // Find the ChapterSelectDynamic instance in the scene
        ChapterSelectDynamic selector = FindObjectOfType<ChapterSelectDynamic>();
        if (selector == null)
        {
            Debug.LogError("ChapterSelectTester: No ChapterSelectDynamic found in the scene!");
            return;
        }

        // Log the current value before change
        Debug.Log($"[Tester] Before: CurrentChapter = {selector.CurrentChapter}");

        // Set it to our test value
        selector.CurrentChapter = testChapter;

        // Log the value after change
        Debug.Log($"[Tester] After: CurrentChapter = {selector.CurrentChapter}");
    }
}
