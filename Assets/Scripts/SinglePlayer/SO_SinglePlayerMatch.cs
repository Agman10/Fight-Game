using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "singlePlayerMatch_", menuName = "Scriptable Objects/Single Player Match")]
public class SO_SinglePlayerMatch : ScriptableObject
{
    public SO_PreFightDialogue preFightDialogue;
    public int characterId;
    public TestPlayer character;
    public SO_Skin defaultColor;
    public SO_Skin alternateColor;

    public int[] stageIds;
    public bool musicIsStageTheme = false;

    public SinglePlayerVictoryQuote PlayerVictoryQuote;
    public SinglePlayerVictoryQuote OpponentVictoryQuote;
}

[Serializable]
public class SinglePlayerVictoryQuote
{
    [TextArea(1, 20)] public string victoryQuote;
    public int[] victoryPoseIds;
    public float textSpeed = 0.05f;
}
