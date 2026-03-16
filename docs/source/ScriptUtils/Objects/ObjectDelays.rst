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
   	   // Calls Destroy() after 2 seconds using realtime.
   	   ObjectDelays.Delay(() => UnityEngine.Object.Destroy(obj), 2, true);
   	   
   	   // flips the value of inputBool to false.
   	   ObjectDelays.Delay(() => inputBool = !inputBool, 2, true);
   	   
   	   // Calls the Debug.Log on the next frame
   	   ObjectDelays.DelayFrame(() => Debug.Log("This is a delayed frame log!"));
   	}
  }
  
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Objects::ObjectDelays
   :members: