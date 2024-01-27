using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    [SerializeField] private GameObject missionObjectEquiped;

    public static inventory Instance = null;

    private bool _missionEquiped=false;

    public bool MissionEquiped
    {
        get => _missionEquiped;

        set
        {
            _missionEquiped = value;
            OnMissionObjectBool.Invoke(_missionEquiped);

        }
    }
    

    [SerializeField] private GameObject funnyObjectEquiped;
    private bool _funnyEquiped = false;

    public bool FunnyEquiped
    {
        get => _funnyEquiped;

        set
        {
            _funnyEquiped = value;
            OnFunnyObjectBool.Invoke(_funnyEquiped);
        }
    }

    [SerializeField] float throwForce;

    bool canInteract = true;

    [SerializeField] float interactionCD = 0.5f;

    PlayerMovement player;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player=GetComponent<PlayerMovement>();
    }

    public void ChangeObject(GameObject obj)
    {
        if(!canInteract)
        {
            return;
        }

        
        

        PickableObject newObject= obj.GetComponent<PickableObject>();

        if ( newObject != null )
        {
            switch (newObject.GetObjectType())
            {
                case pickableObjectType.Mission:

                    if (missionObjectEquiped != null)
                    {
                        missionObjectEquiped.SetActive(true);
                        missionObjectEquiped.transform.localPosition = Vector3.up;
                        missionObjectEquiped.transform.parent = null;
                        missionObjectEquiped.GetComponent<Rigidbody>().AddForce(GenerateRandomDirection() * throwForce, ForceMode.Impulse);

                        SetMissionObject(newObject);
                    }
                    else
                    {
                        SetMissionObject(newObject);
                    }

                    player.GetComponent<Animator>().SetTrigger("Pick-up");
                    canInteract = false;
                    StartCoroutine(StartInteractionCD());
                    break;
                case pickableObjectType.PowerUp:

                    if (funnyObjectEquiped != null)
                    {
                        //funnyObjectEquiped.SetActive(true);
                        //funnyObjectEquiped.transform.localPosition = Vector3.up;
                        //funnyObjectEquiped.transform.parent = null;
                        //funnyObjectEquiped.GetComponent<Rigidbody>().AddForce(GenerateRandomDirection() * throwForce, ForceMode.Impulse);

                        //Debug.Log(GenerateRandomDirection() * throwForce);

                        //SetFunnyObject(newObject);
                    }
                    else
                    {
                        player.GetComponent<Animator>().SetTrigger("Pick-up");
                        SetFunnyObject(newObject);

                        canInteract = false;
                        StartCoroutine(StartInteractionCD());
                    }
                    
                    break;
            }
            

            
        }
    }

    public void SetMissionObject(PickableObject newObject)
    {
        if(newObject!=null)
        {
            missionObjectEquiped = newObject.gameObject;
            missionObjectEquiped.transform.parent = gameObject.transform;
            missionObjectEquiped.transform.localPosition = Vector3.zero;
            missionObjectEquiped.SetActive(false);

            MissionEquiped = true;
        }
        else
        {
            missionObjectEquiped = null;
            MissionEquiped = false;
        }
        
    }

    public void SetFunnyObject(PickableObject newObject)
    {
        if (newObject!=null)
        {
            funnyObjectEquiped = newObject.gameObject;
            funnyObjectEquiped.transform.parent = gameObject.transform;
            funnyObjectEquiped.transform.localPosition = Vector3.zero;
            funnyObjectEquiped.SetActive(false);

            FunnyEquiped = true;

            PlayerMovement.Instance.Animator.SetInteger("AttackIndex", 1);
            PlayerMovement.Instance.PlayPowerUpAttackLoop();
        }
        else
        {
            funnyObjectEquiped = null;
            FunnyEquiped = false;
        }      
    }

    IEnumerator StartInteractionCD()
    {
        yield return new WaitForSeconds(interactionCD);

        canInteract = true;
    }

    public delegate void OnMissionObjectBoolChanged(bool newValue);
    public event OnMissionObjectBoolChanged OnMissionObjectBool;

    public delegate void OnFunnyObjectBoolChanged(bool newValue);
    public event OnFunnyObjectBoolChanged OnFunnyObjectBool;

    public GameObject GetMissionObject()
    {
        return missionObjectEquiped;
    }

    public GameObject GetFunnyObject()
    {
        return funnyObjectEquiped;
    }

    public Vector3 GenerateRandomDirection()
    {
        return new Vector3(Random.Range(-1f,1f), Random.Range(0f,1f),Random.Range(-1f,1f));
    }
}
