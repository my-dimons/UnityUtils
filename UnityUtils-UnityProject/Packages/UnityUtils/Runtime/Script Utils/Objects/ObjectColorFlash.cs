using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  [RequireComponent(typeof(SpriteRenderer))]
  public class ObjectColorFlash : MonoBehaviour {
    [Header("Debug Logs")]

    /// If true, will Debug.Log the color and duration when flashing.
    [SerializeField] private bool logFlash;

    /// Specifies the material type used for color flash rendering effects.
    public enum ColorFlashMaterial {
      Unlit,
      Lit
    }

    /// Represents the default duration, in seconds, for a flash.
    private const float DEFAULT_FLASH_DURATION = 0.1f;

    /// The default <see cref="ColorFlashMaterial"/> to use for flashing if no material is specified."/>
    private const ColorFlashMaterial DEFAULT_FLASH_MATERIAL = ColorFlashMaterial.Lit;

    private const string UNLIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashUnlit";
    private const string LIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashLit";

    private static Material unlitFlashMaterial;
    private static Material litFlashMaterial;

    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;

    void Start() {
      spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Determines whether a flashing operation is currently active.
    /// </summary>
    /// <returns>true if the object is flashing; otherwise, false.</returns>
    public bool IsFlashing() {
      return flashRoutine != null;
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for the specified duration and with a specific material type.
    /// </summary>
    /// <param name="duration">Time to flash white</param>
    /// <param name="flashMaterial">The material to use when flashing the color</param>
    public void FlashWhite(float duration, Material flashMaterial) {
      FlashColor(Color.white, duration, GetMaterialInstance(flashMaterial));
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a specific material type.
    /// </summary>
    public void FlashWhite(Material flashMaterial) {
      FlashWhite(DEFAULT_FLASH_DURATION, GetMaterialInstance(flashMaterial));
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a lit material type.
    /// </summary>
    public void FlashWhite() {
      FlashWhite(DEFAULT_FLASH_DURATION, GetMaterialInstance(GetFlashMaterial(DEFAULT_FLASH_MATERIAL)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    /// <param name="flashMaterial">The material to use when flashing the color</param>
    public void FlashColor(Color color, float duration, Material flashMaterial) {
      if (flashRoutine != null) {
        Debug.LogWarning("Unable to start color flash coroutine");
        StopCoroutine(flashRoutine);
      }

      flashRoutine = StartCoroutine(FlashRoutine(color, duration, GetMaterialInstance(flashMaterial)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time. Uses the default flash material.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    public void FlashColor(Color color, float duration) {
      FlashColor(color, duration, GetMaterialInstance(GetFlashMaterial(DEFAULT_FLASH_MATERIAL)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses the default flash material.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color) {
      FlashColor(color, DEFAULT_FLASH_DURATION, GetMaterialInstance(GetFlashMaterial(DEFAULT_FLASH_MATERIAL)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color, Material flashMaterial) {
      FlashColor(color, DEFAULT_FLASH_DURATION, GetMaterialInstance(flashMaterial));
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
    /// Retrieves the material associated with the specified prebuilt flash material type.
    /// </summary>
    /// <param name="flashMaterial">The type of flash material to get</param>
    /// <returns>The material corresponding to the specified flash material type. Returns the lit material by default</returns>
    public static Material GetFlashMaterial(ColorFlashMaterial flashMaterial) {
      unlitFlashMaterial = Resources.Load<Material>(UNLIT_MATERIAL_PATH);
      litFlashMaterial = Resources.Load<Material>(LIT_MATERIAL_PATH);

      return flashMaterial switch {
        ColorFlashMaterial.Lit => litFlashMaterial,
        ColorFlashMaterial.Unlit => unlitFlashMaterial,
        _ => litFlashMaterial,
      };
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