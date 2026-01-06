using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;
using System;

[Serializable]
public class GameData : ISaveData
{
    public string DataID => nameof(GameData);

    public int intValue;
    public string stringValue;
}
