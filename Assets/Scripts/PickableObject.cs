using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public enum pickableObjectType
{
    Mission,
    Funny
}

public class PickableObject : Interactable
{
    [SerializeField] private string objectName;
    [SerializeField] private Sprite objectSprite;
    [SerializeField] private pickableObjectType objectType;

    bool canInteract = true;

    [SerializeField] float canInteractCD = 0.5f;

    public override void Interact(GameObject interacter)
    {
        base.Interact(interacter);

        inventory playerInventory = interacter.GetComponent<inventory>();

        if (playerInventory != null)
        {
            canInteract = false;

            playerInventory.ChangeObject(gameObject);
        }
    }

    public pickableObjectType GetObjectType()
    {
        return objectType;
    }
}
