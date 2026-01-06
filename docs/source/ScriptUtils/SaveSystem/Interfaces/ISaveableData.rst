ISaveableData
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.SaveSystem`
     
The **ISaveableData** is used in turn with other save systems. **ISaveableData** Is inherited from files that will save and load data. LoadData() and SaveData() are called automatically.
   
Example Usage
-------------
.. code:: csharp
   
   using TMPro;
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;

   public class SaveTesting : MonoBehaviour, ISaveableData
   {
       public int health = 2;
       public string name = "John";

       public TextMeshProUGUI healthText;
       public TextMeshProUGUI nameText;

       public void SaveData<T>(T data) where T : ISaveData
       {
           if (data is PlayerData save)
           {
               save.health = health;
               save.name = name;
           }
       }

       public void LoadData<T>(T data) where T : ISaveData
       {
           if (data is PlayerData save)
           {
               health = save.health;
               name = save.name;
           }
       }

       private void Update()
       {
           healthText.text = "Health: " + health;
           nameText.text = "Name: " + name;
       }
   }   