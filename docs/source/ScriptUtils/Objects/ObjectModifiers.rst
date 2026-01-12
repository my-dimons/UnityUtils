ObjectModifiers
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects`
     
The **ObjectModifiers** script is used in turn with :doc:`ObjectModifierData` to apply modifiers to variables. See :doc:`ModifierType` to check out the modifier types

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
	public float health;
	public ObjectModifiers<float> healthModifiers = new();
	
   	void Start()
   	{
   	   // Apply modifiers.
   	   healthModifiers.AddModifier(new ObjectModifierData<float>(ModifierType.Flat, 12));
   	   healthModifiers.AddModifier(new ObjectModifierData<float>(ModifierType.Multiply, 5));
   	   healthModifiers.AddModifier(new ObjectModifierData<float>(ModifierType.Divide, 4));
   	   
   	   // Print modifiers + modifier order.
   	   healthModifiers.PrintModifierOrder();
   	   healthModifiers.PrintModifiers();
   	   
   	   // Calculate modifiers with a value of 1.
   	   Debug.Log("Value of 1: " + healthModifiers.CalculateModifiers(1)); // Prints 16.25
   	   
   	   // Calculate modifiers with a value of 5.
   	   Debug.Log("Value of 5: " + healthModifiers.CalculateModifiers(1)); // Prints 21.25
   	   
   	   // Adds a modifier temporarily for 1 second.
   	   healthModifiers.AddTemporaryModifier(new ObjectModifierData<float>(ModifierType.Flat, 3), 1);
   	}
   }
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectModifiers<T>
   :members: