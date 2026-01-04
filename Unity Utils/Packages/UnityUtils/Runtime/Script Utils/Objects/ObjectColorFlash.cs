using System.Collections;
using UnityEngine;
using System.IO;

namespace UnityUtils.ScriptUtils.Objects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ObjectColorFlash : MonoBehaviour
    {
        [Header("Debug Logs")]

        /// If true, will Debug.Log the loaded flash material when loaded
        [SerializeField] private bool logLoadMaterial;

        /// If true, will Debug.Log the color and duration when flashing
        [SerializeField] private bool logFlash;

        private const string MATERIAL_PATH = "Materials/ColorFlash";

        private Material flashMaterial;

        private Material originalMaterial;

        private SpriteRenderer spriteRenderer;

        private Color originalColor;

        private Coroutine flashRoutine;

        void Start()
        {
            originalColor = GetComponent<SpriteRenderer>().color;

            spriteRenderer = GetComponent<SpriteRenderer>();

            originalMaterial = spriteRenderer.material;

            flashMaterial = Resources.Load<Material>(MATERIAL_PATH);
            if (logLoadMaterial)
                Debug.Log("Loaded color flash material: " + flashMaterial);

            flashMaterial = new Material(flashMaterial);
        }

        /// <summary>
        /// Flashes the <see cref="SpriteRenderer"/> with a white color for the specified duration.
        /// </summary>
        public void FlashWhite(float duration)
        {
            FlashColor(Color.white, duration);
        }

        /// <summary>
        /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time.
        /// </summary>
        /// <param name="color">Color to switch to</param>
        /// <param name="duration">Time to switch the color for in seconds</param>
        public void FlashColor(Color color, float duration)
        {
            if (flashRoutine != null)
            {
                GetComponent<SpriteRenderer>().color = originalColor;
                StopCoroutine(flashRoutine);
            }

            flashRoutine = StartCoroutine(FlashRoutine(color, duration));
        }

        private IEnumerator FlashRoutine(Color color, float duration)
        {
            spriteRenderer.material = flashMaterial;
            flashMaterial.color = color;

            GetComponent<SpriteRenderer>().color = color;

            if (logFlash)
                Debug.Log("Flashing object. \n color: " + color + "\n duration: " + duration);

            yield return new WaitForSeconds(duration);

            spriteRenderer.material = originalMaterial;

            GetComponent<SpriteRenderer>().color = originalColor;

            flashRoutine = null;
        }
    }
}