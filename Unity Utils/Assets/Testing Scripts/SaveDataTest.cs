using System.Collections;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

[System.Serializable]
public class SaveDataTest : ISaveData
{
    public int val1;
    public string val2;

    public SaveDataTest(SaveTesting data)
    {
        val1 = data.val1;
        val2 = data.val2;
    }
}