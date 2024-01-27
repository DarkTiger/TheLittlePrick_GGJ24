using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public enum pickableObjectType
{
    Mission,
    PowerUp
}

public class PickableObject : Interactable
{
    public string objectName;
    public Sprite objectSprite;
    public pickableObjectType objectType;

    [SerializeField] int funnyLevel=0;

    
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

    public int GetFunnyLevel()
    {
        return funnyLevel;
    }

    public void SetFunnyLevel(int value)
    {
        funnyLevel= value;
    }

    
}
