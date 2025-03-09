using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BookSlot : MonoBehaviour
{
    public int requiredBookID; // The correct book ID for this slot
    private int currentBookID = -1; // -1 means the slot is empty
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private void Start()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        socket.selectEntered.AddListener(OnBookPlaced);
        socket.selectExited.AddListener(OnBookRemoved);
    }

    private void OnBookPlaced(SelectEnterEventArgs args)
    {
        // Get the book component from the placed object
        Book placedBook = args.interactableObject.transform.GetComponent<Book>();

        if (placedBook != null)
        {
            currentBookID = placedBook.bookID;
            Debug.Log($"Book {currentBookID} placed in {gameObject.name}");

            // Check if the correct book is placed
            if (currentBookID == requiredBookID)
            {
                Debug.Log($"✅ Correct book placed in {gameObject.name}!");
            }
            else
            {
                Debug.Log($"❌ Wrong book in {gameObject.name}. Expected ID: {requiredBookID}, but got ID: {currentBookID}");
            }
        }
    }

    private void OnBookRemoved(SelectExitEventArgs args)
    {
        currentBookID = -1; // Reset slot
        Debug.Log($"{gameObject.name} is now empty");
    }

    public bool IsCorrectBookPlaced()
    {
        return currentBookID == requiredBookID;
    }
}
