using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueManager/Dialogue", fileName = "New Dialogue.asset")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]
    public string dialogueLine;
    public AudioClip voiceClip;
    public float dialogueLength;
    public Sprite portrait;

}
