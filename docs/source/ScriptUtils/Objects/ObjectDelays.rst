ObjectDelays
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Objects`
     
The **ObjectDelays** script is used to call functions or modify script properties after a set time

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Objects;
   
   public class ExampleScript : MonoBehaviour
   {
	public GameObject obj;
	public inputBool = true;
	
   	void Start()
   	{
   	   // Destroys an object in 2 seconds with unscaled time.
   	   ObjectDelays.DestroyUnscaledtime(obj, 2);
   	   
   	   // Calls Destroy() after 2 seconds using realtime.
   	   CallFunctionAfterTime(() => UnityEngine.Object.Destroy(obj), 2, true);
   	   
   	   // flips the value of inputBool to false.
   	   ChangeValueAfterTime<bool>(value => inputBool = value, !inputBool, 2, true);
   	}
  }
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectDelays
   :members: