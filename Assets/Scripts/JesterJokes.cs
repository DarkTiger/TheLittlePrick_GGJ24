using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JesterJokes : MonoBehaviour
{
    [SerializeField] AudioClip jokesClip;
    [SerializeField] AudioClip bagClip;
    [SerializeField] AudioClip saxClip;
    [SerializeField] AudioClip lampClip;


    [Header("ObjectToDisable")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject UI;
    

    [SerializeField] Animator animator;

    private PrinceNpc prince;

    private void Start()
    {
        prince=GetComponentInParent<PrinceNpc>();
    }



    public void StartJokesAnimation(MissionObjectEventType jokeObject)
    {
        switch (jokeObject)
        {
            case MissionObjectEventType.Bag:
                animator.SetTrigger("Bag");
                break;
            case MissionObjectEventType.Lamp:
                animator.SetTrigger("Lamp");
                break;
            case MissionObjectEventType.Fax:
                animator.SetTrigger("Sax");
                break;
            case MissionObjectEventType.Jokes:
                animator.SetTrigger("DirtyJokes");
                break;
        }
    }

    public void StartEvent()
    {
        //CamChangePriority(Gamemanager.instance.mainCamera2, Gamemanager.instance.mainCamera1);
        Gamemanager.instance.billBoardEnabled = false;
        Gamemanager.instance.timerStopped = true;
        player.SetActive(false);
        UI.SetActive(false);

        
    }

    public void EndEvent()
    {
        //CamChangePriority(Gamemanager.instance.mainCamera1, Gamemanager.instance.mainCamera2);
        Gamemanager.instance.billBoardEnabled = true;
        Gamemanager.instance.timerStopped = false;
        player.SetActive(true);
        UI.SetActive(true);

        if (CheckWinEvent())
        {
            StartEvent();
        }
        else
        {
            gameObject.SetActive(false);
        }
        
        

        
    }

    public bool CheckWinEvent()
    {
        return Gamemanager.instance.checkForWin();
    }

    public void WinEvent()
    {
        prince.PrinceLaugh();
    }

    

    public void LoseEvent()
    {
        animator.SetTrigger("Lose");
    }

    //public void CamChangePriority(Camera a, Camera b)
    //{
    //    a.tag = "MainCamera";
    //    a.enabled = true;

    //    b.tag = "Untagged";
    //    b.enabled = false;
    //    a.depth = 0;
    //    b.depth = -10;
    //}

}
