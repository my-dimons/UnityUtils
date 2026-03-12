using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityUtils.ScriptUtils
{
    public class CoroutineHelper : MonoBehaviour
    {
        private static CoroutineHelper starter;

        /// Starter for coroutines that don't stop on loading a new scene.
        public static CoroutineHelper Starter
        {
            get
            {
                if (starter == null)
                {
                    GameObject obj = new GameObject("Persistant Coroutine Starter [UnityUtils]");
                    starter = obj.AddComponent<CoroutineHelper>();
                    DontDestroyOnLoad(obj);
                }

                return starter;
            }
        }

        private void Awake()
        {
            if (starter != null && starter != this)
            {
                Destroy(this);
                return;
            }

            starter = this;
            DontDestroyOnLoad(this);

            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }
    }
}