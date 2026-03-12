using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.ScriptUtils.Audio
{
    [RequireComponent(typeof(Slider))]
    public class AudioSlider : MonoBehaviour
    {
        /// Type of audio volume to modify on update.
        public AudioManager.VolumeType volumeType;

        [Space(10)]

        /// If true, this will print a debug log of the updated volume on update. Warning: While being used, this will output lots of Debug.Logs.
        public bool logSliderValueChange = true;

        private Slider slider;

        void Start()
        {
            slider = GetComponent<Slider>();

            slider.onValueChanged.AddListener(OnSliderValueChanged);
            SetSliderValue();
        }

        private void OnSliderValueChanged(float volume)
        {
            AudioManager.SetVolume(volumeType, volume);

            if (logSliderValueChange)
                Debug.Log("Set " + volumeType + " Volume to: " + volume);
        }

        /// <summary>
        /// Sets the slider's volume to the current <see cref="volumeType"/> value
        /// </summary>
        public void SetSliderValue()
        {
            slider.value = AudioManager.GetVolume(volumeType);
        }
    }
}