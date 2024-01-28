using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gardener : MonoBehaviour
{
   
    public DialogueSystem dialogueSystem;
    public PickableObject item;

    private funnyLevelType funnyType;

    
    private void OnTriggerEnter(Collider other)
    {

       

        if (other.gameObject.GetComponent<inventory>())
        {
            List<PickableObject> list = Gamemanager.instance.GetFunnyOrderList();

            foreach (PickableObject p in list)
            {
                if(item == list[0])
                {
                    funnyType = funnyLevelType.neg;
                }
                else if(item== list[list.Count - 1])
                {
                    funnyType = funnyLevelType.pos;
                }
                else
                {
                    funnyType = funnyLevelType.neutro;
                }
            }
            dialogueSystem.PlayDialogue(dialogueSystem.gardenerList, item.GetComponent<MissionObjectType>().EventType(),funnyType);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<inventory>())
            dialogueSystem.StopDialogue();
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();

    }

  
}
