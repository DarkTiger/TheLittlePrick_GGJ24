using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private GameObject owner;
    //[SerializeField] private LayerMask layerToInteract;

    //public void Interaction(PlayerMovement player)
    //{
    //    Collider[] collider = Physics.OverlapSphere(transform.position, radius, layerToInteract);

    //    foreach(Collider collision in collider)
    //    {
    //        Interactable interactableObject=collision.gameObject.GetComponent<Interactable>();

    //        if(interactableObject != null)
    //        {
    //            interactableObject.Interact(player);
    //        }
    //    }


    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}

    private void Start()
    {
        owner = gameObject.transform.parent.gameObject;

        GetComponent<SphereCollider>().radius = radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactableObject=other.gameObject.GetComponent<Interactable>();

        if( interactableObject != null)
        {
            interactableObject.Interact(owner);

            
        }
    }
}
