using UnityEngine;
using System.Collections;

public class MannequinManager : MonoBehaviour
{
    public static MannequinManager Instance;

    [Header("Part Slots")]
    public Transform headSlot;
    public Transform leftArmSlot;
    public Transform rightArmSlot;
    public Transform leftLegSlot;
    public Transform rightLegSlot;
    public Transform torsoSlot;

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        MannequinPart part = other.GetComponent<MannequinPart>();
        if (part != null && part.enabled)  // Only trigger if player is holding a part
        {
            StartCoroutine(SnapWithLerp(part));
        }
    }

    IEnumerator SnapWithLerp(MannequinPart part)
    {
        Transform targetSlot = GetPartSlot(part.partType);

        Vector3 startPos = part.transform.position;
        Quaternion startRot = part.transform.rotation;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3f; // Adjust speed here if needed
            part.transform.position = Vector3.Lerp(startPos, targetSlot.position, t);
            part.transform.rotation = Quaternion.Lerp(startRot, targetSlot.rotation, t);
            yield return null;
        }

        part.SnapToMannequin(targetSlot);
    }

    Transform GetPartSlot(MannequinPart.BodyPartType partType)
    {
        switch (partType)
        {
            case MannequinPart.BodyPartType.Head: return headSlot;
            case MannequinPart.BodyPartType.LeftArm: return leftArmSlot;
            case MannequinPart.BodyPartType.RightArm: return rightArmSlot;
            case MannequinPart.BodyPartType.LeftLeg: return leftLegSlot;
            case MannequinPart.BodyPartType.RightLeg: return rightLegSlot;
            case MannequinPart.BodyPartType.Torso: return torsoSlot;
            default: return null;
        }
    }
}
