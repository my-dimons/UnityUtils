using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  [RequireComponent(typeof(SpriteRenderer))]
  public class ObjectColorFlash : MonoBehaviour {

    [Header("Default Parameters")]
    /// Represents the default duration, in seconds, for a flash.
    [SerializeField] private float defaultFlashDuration = 0.1f;
    ///// The default fade in/out time in seconds.
    //[SerializeField] private float defaultFadeTime = 0f;
    ///// The default intensity (How much the color will flash) for a flash.
    //[Range(0f, 1f)]
    //[SerializeField] private float defaultFlashIntensity = 1f;

    [Space(10)]

    /// The default <see cref="Color"/> that the object will flash to if no color is specified.
    [ColorUsage(true, true)]
    [SerializeField] private Color defaultFlashColor = Color.white;

    [Header("Debug Logs")]
    /// If true, will Debug.Log the color and duration when flashing.
    [SerializeField] private bool logFlash = false;

    private const string SPRITE_MATERIAL_PATH = "Materials/ColorFlash/ColorFlash-Lit-Sprite-MAT"; // Currently unused, but just in case its needed later.

    private static Material defaultFlashSpriteMaterial;

    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;

    void Start() {
      spriteRenderer = GetComponent<SpriteRenderer>();

      defaultFlashSpriteMaterial = Resources.Load<Material>(SPRITE_MATERIAL_PATH);
    }

    /// <summary>
    /// Determines whether a flashing operation is currently active.
    /// </summary>
    /// <returns>true if the object is flashing; otherwise, false.</returns>
    public bool IsFlashing() {
      return flashRoutine != null;
    }


    // <param name="fadeTime">The time it takes to fade in and out the flash color. Default is the <see cref="defaultFadeTime"/></param>
    // <param name="flashAmount">How much the color will flash. Default is the <see cref="defaultFlashIntensity"/></param>
    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/>
    /// </summary>
    /// <param name="color">Color to switch to. Default is the <see cref="defaultFlashColor"/></param>
    /// <param name="duration">Time to switch the color for in seconds. Default is the <see cref="defaultFlashDuration"/></param>
    /// <param name="flashMaterial">The material to use when flashing the color. Default is the <see cref="defaultFlashDuration"/></param>
    public Coroutine Flash(Color color = default, float duration = default, Material flashMaterial = default) {
      if (color == default)
        color = defaultFlashColor;
      if (duration == default)
        duration = defaultFlashDuration;
      //if (flashAmount == default)
      //  flashAmount = defaultFlashIntensity;
      //if (fadeTime == default)
      //  fadeTime = defaultFadeTime;
      if (flashMaterial == default)
        flashMaterial = defaultFlashSpriteMaterial;

      if (flashRoutine != null) {
        Debug.LogWarning("Unable to start color flash coroutine");
        StopCoroutine(flashRoutine);
      }

      flashRoutine = StartCoroutine(FlashRoutine(color, duration, GetMaterialInstance(flashMaterial)));
      return flashRoutine;
    }

    private IEnumerator FlashRoutine(Color color, float duration, Material mat) {
      Material originalMaterial = spriteRenderer.material;
      float currentFlashAmount = 0f;
      float elapsedTime = 0f;

      spriteRenderer.material = mat;
      mat.SetColor("_FlashColor", color);

      if (logFlash)
        Debug.Log("Flashing object " +
          "\n color: " + color +
          "\n duration: " + duration +
          "\n material: " + mat);

      // to be used later
      //while (elapsedTime < duration) {
      //
      //  // iterate elapsedTime
      //  elapsedTime += Time.deltaTime;
      //
      //  // lerp the flash amount
      //  currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / duration);
      //  mat.SetFloat("_FlashAmount", currentFlashAmount);
      //  yield return null;
      //}

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