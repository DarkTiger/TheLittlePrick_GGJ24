using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum funnyLevelType
{
    pos,
    neutro,
    neg
}

public class DialogueSystem : MonoBehaviour
{
    public bool isPlayingDialogue;
    public bool isDialogueSequence;
    public float dialogueTimer = 6f;
    public float skipTimer = 1.5f;
    public Image portrait;
    public bool checkingForNextLine;
    public GameObject dialogueCanvas;
    public int conta;
    public int endDialogue;
    public GameObject skipPrompt;
    public AudioSource voiceSource;
    public AudioClip voiceClip;
    public TextMeshProUGUI dialogueText;
    // Start is called before the first frame update
    public List<Dialogue> princeSequence = new List<Dialogue>();
    public List<Dialogue> bluebellSequence = new List<Dialogue>();
    public List<Dialogue> gardenerList = new List<Dialogue>();
    public List<Dialogue> cookList = new List<Dialogue>();
    public List<Dialogue> witchList = new List<Dialogue>();
    private List<Dialogue> currentSequence = new List<Dialogue>();
    public GameObject Canvas;

    private PickableObject oggetto;


    //public void Start()
    //{
    //    PlaySequence(princeSequence);
    //}


    // Update is called once per frame
    void Update()
    {
        

        if (isDialogueSequence)
        {
            if (Input.GetKey(KeyCode.S))
            {
                SkipDialogue();
            }
        }

        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            if (isDialogueSequence)
            {
                
                CheckForNextLine(currentSequence);
            }

            if (isPlayingDialogue)
            {
                SkipDialogue();
            }

        }

        if (isDialogueSequence)
        {
            dialogueTimer -= Time.deltaTime;
            if (checkingForNextLine)
            {
                skipTimer -= Time.deltaTime;
                if (skipTimer < 0)
                {
                    checkingForNextLine = false;
                    skipTimer = 1f;
                }

            }
            if (dialogueTimer < 0) CheckForNextLine(currentSequence);
        }

       

    }

    

    public void PlayDialogue(List<Dialogue> sequence, MissionObjectEventType type,funnyLevelType funnyType)
    {
        
        dialogueCanvas.SetActive(true);
        isPlayingDialogue = true;

        switch (type)
        {
            case MissionObjectEventType.Bag:
                if (funnyType == funnyLevelType.pos)
                {
                    dialogueText.text = sequence[6].dialogueLine;
                    voiceClip = sequence[6].voiceClip;
                }
                else if (funnyType == funnyLevelType.neg)
                {
                    dialogueText.text = sequence[8].dialogueLine;
                    voiceClip = sequence[8].voiceClip;
                }
                else
                {
                    dialogueText.text = sequence[7].dialogueLine;
                    voiceClip = sequence[7].voiceClip;
                }
                break;
            case MissionObjectEventType.Lamp:
                if (funnyType == funnyLevelType.pos)
                {
                    dialogueText.text = sequence[0].dialogueLine;
                    voiceClip = sequence[0].voiceClip;
                }else if(funnyType == funnyLevelType.neg)
                {
                    dialogueText.text = sequence[2].dialogueLine;
                    voiceClip = sequence[2].voiceClip;
                }
                else
                {
                    dialogueText.text = sequence[1].dialogueLine;
                    voiceClip = sequence[1].voiceClip;
                }
                break;
            case MissionObjectEventType.Fax:
                if (funnyType == funnyLevelType.pos)
                {
                    dialogueText.text = sequence[9].dialogueLine;
                    voiceClip = sequence[9].voiceClip;
                }
                else if (funnyType == funnyLevelType.neg)
                {
                    dialogueText.text = sequence[11].dialogueLine;
                    voiceClip = sequence[11].voiceClip;
                }
                else
                {
                    dialogueText.text = sequence[10].dialogueLine;
                    voiceClip = sequence[10].voiceClip;
                }
                break;
            case MissionObjectEventType.Jokes:
                if (funnyType == funnyLevelType.pos)
                {
                    dialogueText.text = sequence[3].dialogueLine;
                    voiceClip = sequence[3].voiceClip;
                }
                else if (funnyType == funnyLevelType.neg)
                {
                    dialogueText.text = sequence[5].dialogueLine;
                    voiceClip = sequence[5].voiceClip;
                }
                else
                {
                    dialogueText.text = sequence[4].dialogueLine;
                    voiceClip = sequence[4].voiceClip;
                }
                break;
        }
       
        voiceSource.clip = voiceClip;
        voiceSource.Play();
        portrait.overrideSprite = sequence[conta].portrait;

    }

    public void StopDialogue()
    {
        isPlayingDialogue = false;
        dialogueCanvas.SetActive(false);
        Gamemanager.instance.timerStopped = false;  
        isDialogueSequence = false;
        voiceSource.Stop();   
        Canvas.SetActive(true);
        PlayerMovement.Instance.InputEnabled = true;
     

    }

    public void PlaySequence(List<Dialogue> sequence)
    {     
            currentSequence = sequence;
            conta = 0;
            endDialogue = sequence.Count - 1;
            isDialogueSequence = true;
            dialogueText.text = sequence[conta].dialogueLine;
            voiceClip = sequence[conta].voiceClip;
            voiceSource.clip = voiceClip;
        dialogueTimer = sequence[conta].dialogueLength;
        voiceSource.Play();
        portrait.overrideSprite = sequence[conta].portrait;


    }

    private void CheckForNextLine(List<Dialogue> sequence)
    {
        if (!checkingForNextLine)
      
        {
            checkingForNextLine = true;
          
            if (conta >= endDialogue)
            {
                StopDialogue();
            }

            else
            {
                voiceSource.Stop();
                conta++;
                dialogueTimer = sequence[conta].dialogueLength;
                PlayNextDialogueLine(sequence);
            }
        }
    }

    private void PlayNextDialogueLine(List<Dialogue> sequence)
    {
       
        dialogueText.text = sequence[conta].dialogueLine;
        voiceClip = sequence[conta].voiceClip;
        voiceSource.clip = voiceClip;
        voiceSource.Play();
        portrait.overrideSprite = sequence[conta].portrait;
    }

    private void SkipDialogue()
    {
        isPlayingDialogue = false;
        isDialogueSequence = false;
        voiceSource.Stop();
        dialogueCanvas.SetActive(false);
        Canvas.SetActive(true);
        PlayerMovement.Instance.InputEnabled = true;
        Gamemanager.instance.timerStopped = false;

    }

}
