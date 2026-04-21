using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "preFight_", menuName = "Scriptable Objects/Pre Fight Dialogue")]
public class SO_PreFightDialogue : ScriptableObject
{
    public int characterId1;
    public int characterId2;
    public DialogueLine[] dialogueLines;
}

[Serializable]
public class DialogueLine
{
    public float dialogueStayTime = 1f;
    [TextArea(2, 6)] public string charDialogue1;
    public float dialogueSpeed1 = 0.05f;
    [TextArea(2, 6)] public string charDialogue2;
    public float dialogueSpeed2 = 0.05f;
}
