using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenButton : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject dialogueCanvas;

    public void StartGame()
    {
        titleScreen.SetActive(false);
        dialogueCanvas.SetActive(true);
        DialogueSystem dialogue = dialogueCanvas.GetComponent<DialogueSystem>();
        dialogue.PlaySequence(dialogue.princeSequence);
    }
}
