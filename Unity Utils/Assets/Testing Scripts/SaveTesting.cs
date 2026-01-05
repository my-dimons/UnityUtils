using System.Collections;
using TMPro;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveTesting : MonoBehaviour, ISaveableData
{
    public int val1 = 2;
    public string val2 = "Unsaved";

    public TextMeshProUGUI intText;
    public TextMeshProUGUI stringText;
     
    public void Save<T>(T data) where T : ISaveData
    {
        BinarySaveSystem.Save<SaveDataTest, SaveTesting>(this, "saveTest", input => new SaveDataTest(input));
    }

    public void Load<T>(T data) where T : ISaveData
    { 
        if (data is SaveDataTest save)
        {
            val1 = save.val1;
            val2 = save.val2;
        }
    }

    private void Update()
    {
        intText.text = "val1: " + val1;
        stringText.text = "val2: " + val2;
    }
}