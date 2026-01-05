Save System Example
================
        
Example of a Save System
-------------

PlayerSaveData:

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   [System.Serializable]
   public class PlayerSaveData : ISaveData
   {
   	public float health;
   	public float level;
   	
   	public PlayerSaveData(Player player)
   	{
   	   health = player.health;
   	   level = player.level;
   	}
   }

Loading/Saving data:
-----------------

Binary Save System:

.. code:: csharp
   
   using UnityEngine;
   using UnityUtils.ScriptUtils.SaveSystem;
   
   public class Player
   {
   	public float health;
   	public float level;
   	
   	string fileName = "player";
   	
   	public void Save()
   	{
   	   // Creates a file 
   	   BinarySaveSystem.Save<PlayerSaveData, Player>(this, fileName, input => new Player(input));
   	}
   	
   	public void Load()
   	{
   	   PlayerSaveData data = BinarySaveSystem.Load<PlayerSaveData>(fileName);
   	   
   	   health = data.health;
   	   level = data.level;
   	}
   }