using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Particles
{
    public static class ParticleModifier
    {
        /// <summary>
        /// Sets the start color of the specified particle system
        /// </summary>
        public static void SetParticleSystemColor(GameObject particleSystem, Color color)
        {
            var main = GetParticleSystem(particleSystem).main;
            main.startColor = color;
        }

        /// <summary>
        /// Sets the start color of the specified particle system to use the provided gradient
        /// </summary>
        public static void SetParticleSystemGradientColor(GameObject particleSystem, Gradient gradient)
        {
            var main = GetParticleSystem(particleSystem).main;
            main.startColor = gradient;
        }

        /// <summary>
        /// Tries to get the particle system on the given object, prints a warning message if it doesn't exist and returns null
        /// </summary>
        public static ParticleSystem GetParticleSystem(GameObject obj)
        {
            if (!obj.TryGetComponent<ParticleSystem>(out var ps))
            {
                Debug.LogWarning("The \"" + obj.name + "\" Prefab has no ParticleSystem component!");
                return null;
            }

            return ps;
        }
    }
}