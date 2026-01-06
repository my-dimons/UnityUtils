using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

[Serializable]
public class GameData : ISaveData
{
    public int intValue;
    public string stringValue;
}
