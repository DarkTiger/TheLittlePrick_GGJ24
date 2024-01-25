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
    public string objectName;
    public Sprite objectSprite;
    public pickableObjectType objectType;

    public override void Interact(GameObject interacter)
    {
        base.Interact(interacter);

        inventory playerInventory = interacter.GetComponent<inventory>();

        if (playerInventory != null)
        {
            playerInventory.ChangeObject(gameObject);
        }
    }

    public pickableObjectType GetObjectType()
    {
        return objectType;
    }
}
