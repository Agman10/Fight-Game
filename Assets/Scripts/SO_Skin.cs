using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "skin_", menuName = "Scriptable Objects/Skin")]
public class SO_Skin : ScriptableObject
{
    [ColorUsage(true, false)] public Color[] colors;

    [Space]
    [ColorUsage(false, true)] public Color eyeEmission;

    [Space]
    [ColorUsage(false, true)] public Color extraEmission;
    [ColorUsage(true, false)] public Color extraColor = Color.white;
    //public int eyeIndex;
}
