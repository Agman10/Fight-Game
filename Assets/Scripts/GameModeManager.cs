using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance;

    public int gameModeId = 0;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }
}
