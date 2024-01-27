using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaughBarUIVisualizer : MonoBehaviour
{
    [SerializeField] GameObject LaughBar;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            LaughBar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() != null)
        {
            LaughBar.SetActive(false);
        }
    }
}
