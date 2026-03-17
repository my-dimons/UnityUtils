using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonDebugLogs : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [Header("Debug")]

    /// Will output a Debug.Log when the button is hovered over.
    public bool logHover = true;

    /// Will output a Debug.Log when the button's hover is exited.
    public bool logExit = true;

    /// Will output a Debug.Log when the button is clicked.
    public bool logClick = true;

    public void OnPointerEnter(PointerEventData eventData) {
      if (logHover) {
        Debug.Log("Hovered over button!");
      }
    }

    public void OnPointerExit(PointerEventData eventData) {
      if (logExit) {
        Debug.Log("Exited hovering button!");
      }
    }

    public void OnPointerClick(PointerEventData eventData) {
      if (logClick) {
        Debug.Log("Clicked button!");
      }
    }
  }
}