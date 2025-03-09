using UnityEngine;


public class Locker : MonoBehaviour
{
    public Transform insidePosition; // Position inside the locker
    public Transform outsidePosition; // Position outside when exiting
    private bool isHiding = false;
    private float lastHideTime;
    public float hideCooldown = 1.5f;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable lockerDoor;

    private void Start()
    {
        lockerDoor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        lockerDoor.selectEntered.AddListener((args) => ToggleHide());
    }

    private void ToggleHide()
    {
        if (Time.time - lastHideTime < hideCooldown) return; // Prevent spam hiding
        lastHideTime = Time.time;

        if (!isHiding)
        {
            Hide();
        }
        else
        {
            ExitLocker();
        }
    }

    private void Hide()
    {
        isHiding = true;
        // PlayerController.Instance.HideInLocker(insidePosition);
        Debug.Log("🛑 Player is now hiding.");
    }

    private void ExitLocker()
    {
        isHiding = false;
        // PlayerController.Instance.ExitLocker(outsidePosition);
        Debug.Log("✅ Player left the locker.");
    }

    public bool IsPlayerInside()
    {
        return isHiding;
    }
}
