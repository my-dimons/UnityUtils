using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI {
  [RequireComponent(typeof(Button))]
  public class UIButtonToggleObjects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    [Header("Objects")]

    /// Will toggle all objects's active state in this array on hover
    public GameObject[] hoverToggleObjects;

    /// Will toggle all objects's active state in this array on hover exit
    public GameObject[] exitToggleObjects;

    /// Will toggle all objects's active state in this array on click
    public GameObject[] clickToggleObjects;

    [Header("Debug")]
    public bool logToggle;

    public void OnPointerEnter(PointerEventData eventData) {
      ToggleObjects(hoverToggleObjects);
    }

    public void OnPointerExit(PointerEventData eventData) {
      ToggleObjects(exitToggleObjects);
    }

    public void OnPointerClick(PointerEventData eventData) {
      ToggleObjects(clickToggleObjects);
    }

    private void ToggleObjects(GameObject[] objects) {
      foreach (GameObject obj in objects) {
        obj.SetActive(!obj.activeSelf);
      }

      if (logToggle) {
        Debug.Log("Toggled objects: " + objects);
      }
    }
  }
}