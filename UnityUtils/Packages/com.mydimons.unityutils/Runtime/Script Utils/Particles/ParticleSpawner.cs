using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Particles {
  public static class ParticleSpawner {
    /// <summary>
    /// Spawns a burst particle prefab at the given position
    /// Has adjustable color or gradient, but doesn't need to be inputted
    /// </summary>
    /// <param name="particlePrefab">Particle system to spawn (Gameobject with particle system applied)</param>
    /// <param name="position">Position to spawn the particlePrefab at</param>
    /// <param name="parent">Parent to parent the prefab to on spawn</param>
    /// <param name="color">Color to set the particlePrefab to</param>
    /// <param name="gradient">Gradient to set the particlePrefab to</param>
    public static void SpawnBurstParticle(GameObject particlePrefab, Vector3 position, Transform parent = null, Color color = default, Gradient gradient = default) {
      GameObject particleInstance = Object.Instantiate(particlePrefab, position, Quaternion.identity, parent);
      ParticleSystem ps = ParticleModifier.GetParticleSystem(particleInstance);

      if (color != default)
        ParticleModifier.SetParticleSystemColor(particleInstance, color);
      else if (gradient != default)
        ParticleModifier.SetParticleSystemGradientColor(particleInstance, gradient);

      ps.Play();

      float particleLife = ps.main.duration + ps.main.startLifetime.constantMax;
      ObjectDelays.Delay(() => Object.Destroy(particleInstance), particleLife);
    }
  }
}