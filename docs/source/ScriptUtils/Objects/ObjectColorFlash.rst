ObjectColorFlash
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects`
     
Apply a **ObjectColorFlash** script to an object and call its functions to flash it a certain color for a certain time.

.. tip::

   Use ObjectColorFlash.FlashWhite(float duration) to easily apply damage flashes to objects!
   
Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
   	void Start()
   	{
   	   // Flashes the applied object white for 2 seconds.
   	   ObjectColorFlash.FlashWhite(2);
   	   
   	   // Flashes the applied object blue for 2 seconds.
   	   ObjectColorFlash.FlashColor(Color.blue, 2);
   	}
  }
  
  
.. warning::

   ObjectColorFlash only currently works with SpriteRenderers, but Image compatibility is coming soon.
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectColorFlash
   :members: