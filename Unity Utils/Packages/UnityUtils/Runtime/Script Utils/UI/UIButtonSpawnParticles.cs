using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Particles;

namespace UnityUtils.ScriptUtils.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonSpawnParticles : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Particles")]

        /// Will spawn all prefabs in this array on hover
        public GameObject[] hoverParticlePrefabs;

        /// Will spawn all prefabs in this array on hover exit
        public GameObject[] exitParticlePrefabs;

        /// Will spawn all prefabs in this array on click
        public GameObject[] clickParticlePrefabs;

        [Header("Debug")]
        public bool logSpawn;

        public void OnPointerEnter(PointerEventData eventData)
        {
            SpawnParticles(hoverParticlePrefabs);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SpawnParticles(exitParticlePrefabs);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SpawnParticles(clickParticlePrefabs);
        }

        private void SpawnParticles(GameObject[] particles)
        {
            foreach (GameObject particle in particles)
            {
                ParticleSpawner.SpawnBurstParticle(particle, transform.position);
            }

            if (logSpawn)
            {
                Debug.Log("Spawned particle system: " + particles);
            }
        }
    }
}