ObjectColorFlash
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects`
     
Apply a **ObjectColorFlash** script to an object and call its functions to flash it a certain color for a certain time. 
It works by changing the material of the specified object and changing the new materials color, then switching back to the default material after its done.

.. warning::

   Do not modify a objects color or material while flashing, as it may interfere with the flashing. 

.. tip::

   Use ObjectColorFlash.FlashWhite(float duration) to easily apply damage flashes to objects!
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
   	public ObjectColorFlash object;
   	
   	void Start()
   	{
   	   // Flashes the applied object white for 2 seconds.
   	   object.FlashWhite(2);
   	   
   	   // Flashes the applied object blue for 2 seconds.
   	   object.FlashColor(Color.blue, 2);
   	   
   	   // Get the prebuilt in Lit material
   	   Material mat = ObjectColorFlash.GetFlashMaterial(ObjectColorFlash.ColorFlashMaterial.Lit);
   	   
   	   // Check if the object is currently flashing
   	   bool isFlashing = object.IsFlashing();
   	}
  }
  
  
.. warning::

   ObjectColorFlash only currently works with SpriteRenderers, but Image compatibility is coming soon.
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectColorFlash
   :members: