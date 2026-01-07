using TMPro;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveTesting : MonoBehaviour, ISaveableData
{
    public int val1 = 2;
    public string val2 = "Unsaved";

    public TextMeshProUGUI intText;
    public TextMeshProUGUI stringText;

    public void SaveData<T>(T data) where T : SaveData
    {
        if (data is GameData save)
        {
            save.intValue = val1;
            save.stringValue = val2;
        }
    }

    public void LoadData<T>(T data) where T : SaveData
    {
        if (data is GameData save)
        {
            val1 = save.intValue;
            val2 = save.stringValue;
        }
    }

    private void Update()
    {
        intText.text = "val1: " + val1;
        stringText.text = "val2: " + val2;
    }
}