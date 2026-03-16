using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

public class TestingScript3 : MonoBehaviour {
  public GameObject mat1;
  public GameObject mat2;
  public GameObject mat3;

  public Color flashColor;

  private void Start() {
    Invoke(nameof(FlashColors), 1f);
  }

  private void FlashColors() {
    mat1.GetComponent<ObjectColorFlash>().FlashWhite();
    mat2.GetComponent<ObjectColorFlash>().FlashColor(flashColor, 0.5f, ObjectColorFlash.GetFlashMaterial(ObjectColorFlash.ColorFlashMaterial.Lit));
    mat3.GetComponent<ObjectColorFlash>().FlashColor(flashColor, ObjectColorFlash.GetFlashMaterial(ObjectColorFlash.ColorFlashMaterial.Lit));
  }
}
