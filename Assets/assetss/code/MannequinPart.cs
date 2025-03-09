using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MannequinPart : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    public enum BodyPartType { Head, LeftArm, RightArm, LeftLeg, RightLeg, Torso }
    public BodyPartType partType;

    private bool isSnapped = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        Debug.Log($"{partType} picked up!");
    }

    public void SnapToMannequin(Transform targetSlot)
    {
        if (isSnapped) return;

        isSnapped = true;

        interactionLayers = 0;

        // Snap into exact position
        transform.SetParent(targetSlot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Optional: Disable Rigidbody physics
        if (TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
    }
}
