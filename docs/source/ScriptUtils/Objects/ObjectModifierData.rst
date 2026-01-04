ObjectModifierData
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects`
     
The **ObjectModifierData** script is used in turn with :doc:`ObjectModifiers` to apply modifiers to variables

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
	private ObjectModifierData data;
	
   	void Start()
   	{
   	   // Creates a new ObjectModifierData object
   	   data = new ObjectModifierData(ObjectModifiers.ModifierType.Division, 3))
   	}
   }
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectModifierData
   :members: