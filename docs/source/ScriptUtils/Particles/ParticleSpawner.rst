ParticleSpawner
================

**NAMESPACE:**
   `UnityUtils.ScriptUtils.Particles`
     
The **ParticleSpawner** is used to easily play particle systems.

Example Usage
-------------
.. code:: csharp
  
   using UnityEngine;
   using UnityUtils.ScriptUtils.Particles;
   
   public class ExampleScript : MonoBehaviour
   {
   	public GameObject particlePrefab;
   	
   	void Start()
   	{
   	   // Spawn burst particles.
   	   ParticleSpawner.SpawnBurstParticles(particlePrefab, Vector3.zero);
    	}
   }
   
Functions
---------

.. doxygenclass:: UnityUtils::ScriptUtils::Particles::ParticleSpawner
   :members: