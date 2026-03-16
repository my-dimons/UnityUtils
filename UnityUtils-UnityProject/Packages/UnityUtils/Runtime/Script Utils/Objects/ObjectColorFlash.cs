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

    /// If true, will Debug.Log the color and duration when flashing
    [SerializeField] private bool logFlash;

    /// Represents the default duration, in seconds, for a flash.
    private const float DEFAULT_FLASH_DURATION = 0.1f;

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
      FlashColor(Color.white, duration, flashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a specific material type.
    /// </summary>
    public void FlashWhite(Material flashMaterial) {
      FlashWhite(DEFAULT_FLASH_DURATION, flashMaterial);
    }

    /// <summary>
    /// Flashes the <see cref="SpriteRenderer"/> with a white color for 0.1s and with a lit material type.
    /// </summary>
    public void FlashWhite() {
      FlashWhite(DEFAULT_FLASH_DURATION, GetFlashMaterial(ColorFlashMaterial.Lit));
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
        //GetComponent<SpriteRenderer>().color = originalColor;
        StopCoroutine(flashRoutine);
      }

      flashRoutine = StartCoroutine(FlashRoutine(color, duration, flashMaterial));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for a set time. Uses a lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    /// <param name="duration">Time to switch the color for in seconds</param>
    public void FlashColor(Color color, float duration) {
      FlashColor(color, duration, GetFlashMaterial(ColorFlashMaterial.Lit));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses a lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color) {
      FlashColor(color, DEFAULT_FLASH_DURATION, GetFlashMaterial(ColorFlashMaterial.Lit));
    }

    /// <summary>
    /// Flashes a <see cref="SpriteRenderer"/> to a certain color for 0.1s. Uses a lit flash material by default.
    /// </summary>
    /// <param name="color">Color to switch to</param>
    public void FlashColor(Color color, Material flashMaterial) {
      FlashColor(color, DEFAULT_FLASH_DURATION, flashMaterial);
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

      if (unlitFlashMaterial != null)
        unlitFlashMaterial = new Material(unlitFlashMaterial);
      if (litFlashMaterial != null)
        litFlashMaterial = new Material(litFlashMaterial);

      return flashMaterial switch {
        ColorFlashMaterial.Lit => litFlashMaterial,
        ColorFlashMaterial.Unlit => unlitFlashMaterial,
        _ => litFlashMaterial,
      };
    }
  }
}