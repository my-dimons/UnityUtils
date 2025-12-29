using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UnityUtils.ScriptUtils.UI
{
    public class UIButtonSceneSwitcher : MonoBehaviour, IPointerClickHandler
    {
        [Header("Scene Loading (Only use 1 of the below variables, leave 1 default)")]

        /// If this value is not empty, it will load the scene on click using the sceneName 
        public string sceneName = "";

        /// If this value is not -1 (Default value), it will load the scene using the buildIndex
        public int buildIndex = -1;

        [Header("Scene Mode")]

        /// The <see cref="LoadSceneMode"/> to use when loading the scene
        public LoadSceneMode sceneMode;

        [Header("Debug")]

        /// If true, will do a Debug.Log when switching scenes
        public bool logSwitch;

        public void OnPointerClick(PointerEventData eventData)
        {
            LoadScene();
        }

        private void LoadScene()
        {
            bool useSceneName = sceneName != "";
            bool useBuildIndex = buildIndex != -1;

            if (useSceneName && !useBuildIndex)
                SceneManager.LoadScene(sceneName, sceneMode);
            else if (useBuildIndex && !useSceneName)
                SceneManager.LoadScene(buildIndex, sceneMode);
            else
                Debug.Log("Cannot load scene, sceneName and buildIndex are both being used. Change one value to be null to load the scene");

            if (logSwitch)
                Debug.Log("Switching Scenes");
        }
    }
}