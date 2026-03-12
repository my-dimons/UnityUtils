using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Audio;

namespace UnityUtils.ScriptUtils.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Audio Clips & Volumes")]

        /// Sound to play when the mouse cursor enters the buttons hitbox.
        public AudioClip hoverEnterSfx;
        /// Volume to play <see cref="hoverEnterSfx"/> at.
        public float hoverEnterVolume = AudioManager.MAX_AUDIO_VOLUME;

        [Space(5)]

        /// Sound to play when the mouse cursor exits the buttons hitbox.
        public AudioClip hoverExitSfx;
        /// Volume to play <see cref="hoverExitSfx"/> at.
        public float hoverExitVolume = AudioManager.MAX_AUDIO_VOLUME;

        [Space(5)]

        /// Sound to play when the mouse clicks on the button.
        public AudioClip clickSfx;

        /// Volume to play <see cref="clickSfx"/> at.
        public float clickVolume = AudioManager.MAX_AUDIO_VOLUME;

        [Header("Adjustable Values")]

        /// Random variance to play all audio clips at.
        public float pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

        /// Type of audio to use to get proper audio levels.
        public AudioManager.VolumeType volumeType = AudioManager.VolumeType.Sfx;

        private bool sceneLoadTriggered;

        [Header("Debug")]

        /// If true this will log errors when trying to play sound effects but they become empty
        public bool logEmptySfx;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (hoverEnterSfx != null)
                SfxManager.PlaySfxAudioClip(hoverEnterSfx, hoverEnterVolume, pitchVariance, volumeType);
            else if (logEmptySfx)
                Debug.LogWarning("No hover enter SFX on button!");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (sceneLoadTriggered)
                return;

            if (hoverExitSfx != null)
                SfxManager.PlaySfxAudioClip(hoverExitSfx, hoverExitVolume, pitchVariance, volumeType);
            else if (logEmptySfx)
                Debug.LogWarning("No hover exit SFX on button!");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GetComponent<UIButtonSceneSwitcher>())
            {
                sceneLoadTriggered = true;
                return;
            }

            if (clickSfx != null)
                SfxManager.PlaySfxAudioClip(clickSfx, clickVolume, pitchVariance, volumeType);
            else if (logEmptySfx)
                Debug.LogWarning("No click SFX on button!");
        }
    }
}
