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
    [TextArea(2, 6)] public string charDialogue1;
    [TextArea(2, 6)] public string charDialogue2;
}
