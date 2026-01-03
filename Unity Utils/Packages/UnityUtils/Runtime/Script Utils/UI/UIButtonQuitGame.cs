using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonQuitGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Objects")]

        /// Will quit the application on hover
        public bool hoverQuit;

        /// Will quit the application on hover exit
        public bool exitQuit;

        /// Will quit the application on click
        public bool clickQuit = true;

        [Header("Debug")]
        public bool logQuit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (hoverQuit)
                QuitGame();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (exitQuit)
                QuitGame();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (clickQuit)
                QuitGame();
        }

        private void QuitGame()
        {
            if (logQuit)
                Debug.Log("Quitting Application");

            Application.Quit();
        }
    }
}