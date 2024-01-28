using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenButton : MonoBehaviour
{
    public GameObject titleScreen;
    public GameObject dialogueCanvas;
    public GameObject dialogueManager;

    public void StartGame()
    {
        titleScreen.SetActive(false);
        dialogueCanvas.SetActive(true);
        DialogueSystem dialogue = dialogueManager.GetComponent<DialogueSystem>();
        dialogue.PlaySequence(dialogue.princeSequence);
        Cursor.visible = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
