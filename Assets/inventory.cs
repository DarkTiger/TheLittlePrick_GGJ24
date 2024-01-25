using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    [SerializeField] private GameObject missionObjectEquiped;
    bool missionEquiped=false;

    [SerializeField] private GameObject funnyObjectEquiped;
    bool funnyEquiped = false;

    [SerializeField] float throwForce;

    bool canInteract = true;

    [SerializeField] float interactionCD = 0.5f;

    

    public void ChangeObject(GameObject obj)
    {
        if(!canInteract)
        {
            return;
        }

        PickableObject newObject= obj.GetComponent<PickableObject>();

        if ( newObject != null )
        {
            if(newObject.GetObjectType()== pickableObjectType.Mission)
            {
                if(missionObjectEquiped!=null)
                {
                    missionObjectEquiped.SetActive(true);
                    missionObjectEquiped.transform.parent = null;
                    missionObjectEquiped.GetComponent<Rigidbody>().AddExplosionForce(throwForce, new Vector3(transform.localScale.x, 0), 0.3f);

                    SetMissionObject(newObject);
                }
                else
                {
                    SetMissionObject(newObject);
                }

                canInteract = false;
                StartCoroutine(StartInteractionCD());
            }
            else if(newObject.GetObjectType() == pickableObjectType.Funny)
            {
                if (funnyObjectEquiped != null)
                {
                    funnyObjectEquiped.SetActive(true);
                    funnyObjectEquiped.transform.parent = null;
                    funnyObjectEquiped.GetComponent<Rigidbody>().AddForce(Vector3.right * throwForce);

                    SetFunnyObject(newObject);
                }
                else
                {
                    SetFunnyObject(newObject);
                }
            }

            canInteract= false;
            StartCoroutine(StartInteractionCD());
        }
    }

    private void SetMissionObject(PickableObject newObject)
    {
        missionObjectEquiped = newObject.gameObject;
        missionObjectEquiped.transform.parent = gameObject.transform;
        missionObjectEquiped.transform.localPosition = Vector3.zero;
        missionObjectEquiped.SetActive(false);
    }

    private void SetFunnyObject(PickableObject newObject)
    {
        funnyObjectEquiped = newObject.gameObject;
        funnyObjectEquiped.transform.parent = gameObject.transform;
        funnyObjectEquiped.transform.localPosition = Vector3.zero;
        funnyObjectEquiped.SetActive(false);
    }

    IEnumerator StartInteractionCD()
    {
        yield return new WaitForSeconds(interactionCD);

        canInteract = true;
    }
}
