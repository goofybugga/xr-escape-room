using UnityEngine;

public class BookOrderingPuzzleManager : MonoBehaviour
{
    public BookSlot[] bookSlots; // Assign all book slots in the Inspector
    public GameObject doorToUnlock; // Assign the door GameObject
    private bool puzzleSolved = false; // Prevent multiple triggers

    private void Start()
    {
        Debug.Log("📌 Puzzle Manager Initialized! Slots Count: " + bookSlots.Length);
    }

    private void Update()
    {
        if (!puzzleSolved) // Only check if puzzle is not yet solved
        {
            CheckBooks();
        }
    }

    private void CheckBooks()
    {
        foreach (BookSlot slot in bookSlots)
        {
            if (!slot.IsCorrectBookPlaced())
            {
                return; // If one book is incorrect, do nothing
            }
        }

        // If all books are placed correctly, solve the puzzle
        puzzleSolved = true;
        Debug.Log("✅ All books are placed correctly! Puzzle solved!");
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        Debug.Log("🚪 Door Unlocked!");

        if (doorToUnlock != null)
        {
            Animator animator = doorToUnlock.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Open"); // Ensure the Animator has an "Open" trigger
            }
        }
    }
}
