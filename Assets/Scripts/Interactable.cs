using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Interactable : MonoBehaviour
{
    [SerializeField] private UnityEvent onInteraction;

    public virtual void Interact(GameObject interacter)
    {
        onInteraction?.Invoke();

        Debug.Log("Ho interagito");
    }
}
