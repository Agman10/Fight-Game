using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    public TestPlayer player1;
    public TestPlayer player2;

    public int winnerCharacterId;
    public int loserCharacterId;

    public SO_Skin player1Skin;
    public SO_Skin player2Skin;

    public bool winnerWasPlayer1;

    [Space]
    public int player1Id;
    public int player2Id;
    public bool draw;

    [Space]
    public int musicTypeId;

    [Space]
    public bool vsAi = false;
    public int vsAiId = 0;

    [Space]
    public bool stageChosen;
    public int stageId = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }
}
