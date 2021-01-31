using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpObjectBehavior : MonoBehaviour
{
    [SerializeField]
    private Transform pickUpSlot;
    [SerializeField]
    private float sphereCastRadius = 0.4f;
    [SerializeField]
    private float sphereCastMaxDistance = 1f;

    private PickUpableItem pickedUpItem;
    private LayerMask pickUpableItemsLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        pickUpableItemsLayerMask = LayerMask.GetMask("PickUpableItems");

        if (pickUpSlot == null) {
            Debug.LogError("Missing pickUpSlot!");
        }
    }

    void OnPickUpDrop()
    {

        if (pickedUpItem)
        {
            // Debug.Log("Dropping");
            pickedUpItem.OnDropped();
            pickedUpItem = null;
        }
        else
        {
            // Debug.Log("Maybe picking up");
            // Debug.DrawRay(transform.position, transform.forward * (sphereCastRadius + sphereCastMaxDistance), Color.red, 2);
            RaycastHit hit;
            if (Physics.SphereCast(
                transform.position,
                sphereCastRadius,
                transform.forward,
                out hit,
                sphereCastMaxDistance,
                pickUpableItemsLayerMask))
            {
                Debug.Log("Picking up: " + hit.transform.name);
                pickedUpItem = hit.transform.GetComponent<PickUpableItem>();
                pickedUpItem.OnPickedUp(pickUpSlot);
            }
        }
    }
}
