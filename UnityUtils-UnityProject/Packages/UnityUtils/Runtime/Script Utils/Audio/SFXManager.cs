using UnityEngine;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.Audio
{
    public static class SfxManager
    {
        /// <summary>
        /// Plays an <see cref="AudioClip"/> globally (non-spacialy) at a certain volume with a pitch variance (to feel less repetative).
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play.</param>
        /// <param name="volume">Playback volume.</param>
        /// <param name="pitchVariance">
        /// Random variance to make sound feel less repetative. 
        /// randomly modifies the pitch of the Music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="AudioManager.DEFAULT_PITCH_VARIANCE"/> is the default value).
        /// </param>
        public static void PlaySfxAudioClip(AudioClip clip, float volume = 1, float pitchVariance = default, AudioManager.VolumeType type = AudioManager.VolumeType.Sfx)
        {
            if (pitchVariance == default) pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

            CreateAndPlayAudioClip(clip, volume, pitchVariance, type: type);
        }

        /// <summary>
        /// Plays an <see cref="AudioClip"/> globally (non-spacialy) at a certain volume for a set amount of time.
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play</param>
        /// <param name="time">Specified time for <see cref="AudioClip"/> to play for in seconds.</param>
        /// <param name="volume">Playback volume.</param>
        public static void PlayTimedSFXAudioClip(AudioClip clip, float time, float volume = 1, AudioManager.VolumeType type = AudioManager.VolumeType.Sfx)
        {
            float clipLength = AudioManager.CalculateClipPitchWithLength(clip.length, time);

            CreateAndPlayAudioClip(clip, volume, pitch: clipLength, type: type);
        }

        /// <summary>
        /// Plays an <see cref="AudioClip"/> spacially (Changes volume and L/R volume channel based on location) at a certain volume with a pitch variance (to feel less repetative).
        /// </summary>
        /// <param name="clip"><see cref="AudioClip"/> to play.</param>
        /// <param name="position">Position to play <see cref="AudioClip"/> at.</param>
        /// <param name="volume">Playback volume.</param>
        /// <param name="pitchVariance">
        /// Random variance to make sound feel less repetative. 
        /// randomly modifies the pitch of the Music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="DEFAULT_PITCH_VARIANCE"/> is the default value).
        /// </param>
        public static void PlaySpacialSfxAudioClip(AudioClip clip, Vector3 position, float volume = 1, float pitchVariance = default, AudioManager.VolumeType type = AudioManager.VolumeType.Sfx)
        {
            if (pitchVariance == default) pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

            CreateAndPlayAudioClip(clip, volume, pitchVariance, position: position, type: type);
        }

        /// <summary>
        /// Plays an <see cref="AudioClip"/> on an already existing <see cref="AudioSource"/> at a certain volume with a pitch variance (to feel less repetative).
        /// </summary>
        /// <param name="volume">Playback volume.</param>
        /// <param name="audioType">used to get the proper volume, see <see cref="AudioManager.CalculateVolumeBasedOnType(float, AudioManager.VolumeType)"/> to get more info.</param>
        /// <param name="pitchVariance"> 
        /// Random variance to make sound feel less repetative. 
        /// randomly modifies the pitch of the Music<see cref="AudioSource"/> in a random of 1 - pitchVariance, and 1 + pitchVariance (<see cref="DEFAULT_PITCH_VARIANCE"/> is the default value).
        /// </param>
        public static void PlayClipOnSource(AudioClip clip, AudioSource source, float volume = 1, float pitchVariance = default, AudioManager.VolumeType audioType = AudioManager.VolumeType.Sfx)
        {
            if (pitchVariance == default) pitchVariance = AudioManager.DEFAULT_PITCH_VARIANCE;

            PlayAudioClipOnSource(clip, source, volume, pitchVariance, audioType);
        }

        private static void CreateAndPlayAudioClip(AudioClip clip, float volume = 1, float pitchVariance = 0, float pitch = default, Vector3 position = default, Transform parent = default, AudioManager.VolumeType type = AudioManager.VolumeType.Sfx)
        {
            if (type == default) type = AudioManager.VolumeType.Sfx;

            const int SOUND_2D = 0, SOUND_3D = 1;

            GameObject temporaryGameObject = new GameObject("Temporary Audio Clip [UnityUtils]");
            AudioSource audioSource = temporaryGameObject.AddComponent<AudioSource>();

            if (parent == default)
                temporaryGameObject.transform.parent = null;
            else
                temporaryGameObject.transform.parent = parent;

            // set to Global or spacial audio
            if (position == default)
            {
                temporaryGameObject.transform.position = Camera.main.transform.position;
                audioSource.spatialBlend = SOUND_2D;
            }
            else
            {
                temporaryGameObject.transform.position = position;
                audioSource.spatialBlend = SOUND_3D;
            }

            if (pitch == default)
                PlayAudioClipOnSource(clip, audioSource, volume, pitchVariance, type);
            else
                PlayAudioClipOnSource(clip, audioSource, volume, 0f, type, pitch);

            float destroyTime = AudioManager.CalculateClipLength(clip.length, audioSource.pitch);
            ObjectDelays.DestroyUnscaledtime(temporaryGameObject, destroyTime);
        }

        private static void PlayAudioClipOnSource(AudioClip audioClip, AudioSource audioSource, float volume, float pitchVariance = default, AudioManager.VolumeType audioType = AudioManager.VolumeType.Sfx, float pitch = default)
        {
            audioSource.clip = audioClip;
            audioSource.volume = AudioManager.CalculateVolumeBasedOnType(volume, audioType);

            float randomPitch = AudioManager.CalculatePitchVariance(pitchVariance);
            float usedPitch = pitch == default ? randomPitch : pitch;
            audioSource.pitch = usedPitch;

            audioSource.Play();
        }
    }
}