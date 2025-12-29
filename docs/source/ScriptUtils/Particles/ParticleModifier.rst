ParticleModifier
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Particles`
     
The **ParticleModifier** is used to easily play particle systems.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Particles;
   
   public class ExampleScript : MonoBehaviour
   {
   	public GameObject particles;
   	
   	void Start()
   	{
   	   // Set particle system colour.
   	   ParticleModifier.SetParticleSystemColor(particles, Color.blue);
   	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Particles::ParticleModifier
   :members: