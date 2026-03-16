using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  [RequireComponent(typeof(SpriteRenderer))]
  public class ObjectColorFlash : MonoBehaviour {
    /// Specifies the material type used for color flash rendering effects.
    public enum ColorFlashMaterial {
      Unlit,
      Lit
    }

    [Header("Debug Logs")]

    /// If true, will Debug.Log the loaded flash material when loaded
    [SerializeField] private bool logLoadMaterial;

    /// If true, will Debug.Log the color and duration when flashing
    [SerializeField] private bool logFlash;

    /// Represents the default duration, in seconds, for a flash effect.
    private const float DEFAULT_FLASH_DURATION = 0.1f;

    private const string UNLIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashUnlit";
    private const string LIT_MATERIAL_PATH = "Materials/ColorFlash/ColorFlashLit";

    private Material unlitFlashMaterial;
    private Material litFlashMaterial;

    private SpriteRenderer spriteRenderer;

    private Coroutine flashRoutine;

    void Start() {
      spriteRenderer = GetComponent<SpriteRenderer>();

      unlitFlashMaterial = Resources.Load<Material>(UNLIT_MATERIAL_PATH);
      if (logLoadMaterial)
        Debug.Log("Loaded color flash material: " + unlitFlashMaterial);

      litFlashMaterial = Resources.Load<Material>(LIT_MATERIAL_PATH);
      if (logLoadMaterial)
        Debug.Log("Loaded color flash material: " + litFlashMaterial);

      if (unlitFlashMaterial != null)
        unlitFlashMaterial = new Material(unlitFlashMaterial);

      if (litFlashMaterial != null)
        litFlashMaterial = new Material(litFlashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for the specified duration and with a specific material type.
    /// </summary>
    /// <param name="duration">Time to flash white</param>
    /// <param name="flashMaterialType">The material type to use when flashing the color</param>
    public void FlashWhite(float duration, ColorFlashMaterial flashMaterialType) {
      FlashColor(Color.white, duration, flashMaterialType);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a specific material type.
    /// </summary>
    public void FlashWhite(ColorFlashMaterial flashMaterialType) {
      FlashWhite(DEFAULT_FLASH_DURATION, flashMaterialType);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a lit material type.
    /// </summary>
    public void FlashWhite() {
      FlashWhite(DEFAULT_FLASH_DURATION, ColorFlashMaterial.Lit);
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    /// <param name="flashMaterialType">The material type to use when flashing the color</param>
    public void FlashColor(Color color, float duration, ColorFlashMaterial flashMaterialType) {
      if (flashRoutine != null) {
        Debug.LogWarning("Unable to start color flash coroutine");
        //GetComponent<SpriteRenderer>().color = originalColor;
        StopCoroutine(flashRoutine);
      }

      flashRoutine = StartCoroutine(FlashRoutine(color, duration, GetFlashMaterial(flashMaterialType)));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time. Uses the lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    public void FlashColor(Color color, float duration) {
      FlashColor(color, duration, ColorFlashMaterial.Lit);
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses the lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color) {
      FlashColor(color, DEFAULT_FLASH_DURATION, ColorFlashMaterial.Lit);
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses the lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color, ColorFlashMaterial flashMaterialType) {
      FlashColor(color, DEFAULT_FLASH_DURATION, flashMaterialType);
    }

    private IEnumerator FlashRoutine(Color color, float duration, Material mat) {
      spriteRenderer.material = mat;
      mat.color = color;

      Color originalColor = GetComponent<SpriteRenderer>().color;
      Material originalMaterial = spriteRenderer.material;

      GetComponent<SpriteRenderer>().color = color;

      if (logFlash)
        Debug.Log("Flashing object. \n color: " + color + "\n duration: " + duration + "\n material: " + mat);

      yield return new WaitForSeconds(duration);

      spriteRenderer.material = originalMaterial;
      GetComponent<SpriteRenderer>().color = originalColor;

      flashRoutine = null;
    }

    /// <summary>
    /// Retrieves the material associated with the specified flash material type.
    /// </summary>
    /// <param name="flashMaterial">The type of flash material to get</param>
    /// <returns>The material corresponding to the specified flash material type. Returns the lit material by default</returns>
    public Material GetFlashMaterial(ColorFlashMaterial flashMaterial) {
      return flashMaterial switch {
        ColorFlashMaterial.Lit => litFlashMaterial,
        ColorFlashMaterial.Unlit => unlitFlashMaterial,
        _ => litFlashMaterial,
      };
    }
  }
}