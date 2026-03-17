using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  [RequireComponent(typeof(SpriteRenderer))]
  public class ObjectColorFlash : MonoBehaviour {

    [Header("Default Parameters")]
    /// Represents the default duration, in seconds, for a flash.
    [SerializeField] private float defaultFlashDuration = 0.1f;
    /// The default <see cref="Color"/> that the object will flash to if no color is specified.
    [SerializeField] private Color defaultFlashColor = Color.white;

    [Header("Debug Logs")]
    /// If true, will Debug.Log the color and duration when flashing.
    [SerializeField] private bool logFlash;

    private const string UNLIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashUnlit";
    private const string LIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashLit"; // Currently unused, but just in case its needed later.

    private static Material defaultFlashMaterial;

    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;

    void Start() {
      spriteRenderer = GetComponent<SpriteRenderer>();

      defaultFlashMaterial = Resources.Load<Material>(UNLIT_MATERIAL_PATH);
    }

    /// <summary>
    /// Determines whether a flashing operation is currently active.
    /// </summary>
    /// <returns>true if the object is flashing; otherwise, false.</returns>
    public bool IsFlashing() {
      return flashRoutine != null;
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time and with a certain material.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    /// <param name="flashMaterial">The material to use when flashing the color</param>
    public void Flash(Color color, float duration, Material flashMaterial) {
      if (flashRoutine != null) {
        Debug.LogWarning("Unable to start color flash coroutine");
        StopCoroutine(flashRoutine);
      }

      flashRoutine = StartCoroutine(FlashRoutine(color, duration, GetMaterialInstance(flashMaterial)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time. Uses the <see cref="defaultFlashMaterial"/>.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    public void Flash(Color color, float duration) {
      Flash(color, duration, defaultFlashMaterial);
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for the <see cref="defaultFlashDuration"/>
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="flashMaterial">The material to use when flashing the color</param>
    public void Flash(Color color, Material flashMaterial) {
      Flash(color, defaultFlashDuration, flashMaterial);
    }


    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for the specified duration and with a specific material type.
    /// </summary>
    /// <param name="duration">Time to flash white</param>
    /// <param name="flashMaterial">The material to use when flashing the color</param>
    public void Flash(float duration, Material flashMaterial) {
      Flash(Color.white, duration, flashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a specific material type.
    /// </summary>
    public void Flash(Material flashMaterial) {
      Flash(Color.white, defaultFlashDuration, flashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for a set duration and the <see cref="defaultFlashMaterial"/>.
    /// </summary>
    public void Flash(float duration) {
      Flash(Color.white, duration, defaultFlashMaterial);
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses the default flash material.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void Flash(Color color) {
      Flash(color, defaultFlashDuration, defaultFlashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for <see cref="defaultFlashDuration"/> and the <see cref="defaultFlashMaterial"/>.
    /// </summary>
    public void Flash() {
      Flash(defaultFlashDuration, defaultFlashMaterial);
    }

    private IEnumerator FlashRoutine(Color color, float duration, Material mat) {
      Material originalMaterial = spriteRenderer.material;

      spriteRenderer.material = mat;
      mat.color = color;

      if (logFlash)
        Debug.Log("Flashing object \n color: " + color + "\n duration: " + duration + "\n material: " + mat);

      yield return new WaitForSeconds(duration);

      if (logFlash)
        Debug.Log("Finished flashing object");

      spriteRenderer.material = originalMaterial;

      flashRoutine = null;
    }

    /// <summary>
    /// Creates a new instance of the specified material, or returns null if the input is null.
    /// </summary>
    /// <remarks>A warning is logged if the input material is null. The returned material is a separate
    /// instance and changes to it do not affect the original material.</remarks>
    /// <param name="material">The material to duplicate</param>
    /// <returns>A new instance of the specified material, or null if the input material is null.</returns>
    private static Material GetMaterialInstance(Material material) {
      if (material != null)
        return new Material(material);
      else {
        Debug.LogWarning("Unable to get material instance");
        return null;
      }
    }
  }
}