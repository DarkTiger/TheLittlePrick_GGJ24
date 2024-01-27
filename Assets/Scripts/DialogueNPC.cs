using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    [SerializeField] GameObject BoxTestPrefab;
    
    [SerializeField] Sprite characterSprite;

    [SerializeField] int dialogueSpeed = 50;

    bool isPhrasing=false;

    private void OnTriggerEnter(Collider other)
    {
        if (isPhrasing)
        {
            return;
        }

        PlayerMovement player=other.GetComponent<PlayerMovement>();

        if(player != null)
        {           
            isPhrasing = true;
            GameObject boxtest = Instantiate(BoxTestPrefab);

            boxtest.transform.position = new Vector3(transform.position.x, boxtest.transform.position.y+ transform.position.y,transform.position.z);
            boxtest.GetComponent<UIBoxTest>().SetTextSpeed(dialogueSpeed);
            boxtest.GetComponent<UIBoxTest>().StartSentece("banannananana");

            
        }
    }


}
