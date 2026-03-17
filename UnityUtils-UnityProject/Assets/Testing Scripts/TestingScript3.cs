using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

public class TestingScript3 : MonoBehaviour {
  public GameObject mat1;
  public GameObject mat2;
  public GameObject mat3;

  public bool mat2Flashing;

  public Color flashColor;

  private void Start() {
    Invoke(nameof(FlashColors), 1f);
  }

  private void FlashColors() {
    mat1.GetComponent<ObjectColorFlash>().Flash();
    mat2.GetComponent<ObjectColorFlash>().Flash(flashColor, 0.5f, ObjectColorFlash.GetFlashMaterial(ObjectColorFlash.ColorFlashMaterial.Lit));
    mat3.GetComponent<ObjectColorFlash>().Flash(flashColor, ObjectColorFlash.GetFlashMaterial(ObjectColorFlash.ColorFlashMaterial.Lit));
  }

  private void Update() {
    mat2Flashing = mat2.GetComponent<ObjectColorFlash>().IsFlashing();
  }
}
