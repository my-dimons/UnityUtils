CoroutineHelper
==========

**NAMESPACE:**
   `UnityUtils.ScriptUtils`
     
The **CoroutineHelper** script is used to start coroutines in a static class. Coroutines are stopped on scene load.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils;
   using System;
   using System.Collections;
   
   public static class ExampleScript
   {
   	void ExampleFunction()
   	{
   	   // Start a coroutine.
   	   CoroutineHelper.Starter.StartCoroutine(ExampleCoroutine());
   	}
   	
   	public IEnumerator ExampleCoroutine()
   	{
   	   yield return new WaitForSeconds(3f);
   	   Debug.Log("Coroutine Ended");
   	}
   }  
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::CoroutineHelper
   :members:
   :exclude-members: Starter