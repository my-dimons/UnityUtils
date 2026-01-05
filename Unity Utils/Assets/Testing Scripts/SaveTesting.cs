using System.Collections;
using TMPro;
using UnityEngine;
using UnityUtils.ScriptUtils.SaveSystem;

public class SaveTesting : MonoBehaviour
{
    public int val1 = 2;
    public string val2 = "Unsaved";

    public TextMeshProUGUI intText;
    public TextMeshProUGUI stringText;

    public void Save()
    {
        BinarySaveSystem.Save<SaveDataTest, SaveTesting>(this, "saveTest", input => new SaveDataTest(input));
    }

    public void Load()
    {
        SaveDataTest data = BinarySaveSystem.Load<SaveDataTest>("saveTest");

        val1 = data.val1;
        val2 = data.val2;
    }

    private void Update()
    {
        intText.text = "val1: " + val1;
        stringText.text = "val2: " + val2;
    }
}