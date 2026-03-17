using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Audio {
  /// <summary>
  /// Holds variables/functions for other audio scripts (Such as Global volume)
  /// </summary>
  public static class AudioManager {
    /// Maximum audio volume.
    public const float MAX_AUDIO_VOLUME = 1f;

    /// Minimum audio volume.
    public const float MIN_AUDIO_VOLUME = 0f;

    /// Default pitch variance used around the package.
    public const float DEFAULT_PITCH_VARIANCE = 0.1f;

    /// Holds different audio types for volume calculations
    public enum VolumeType {
      Global,
      Sfx,
      Music,
      UI,
      Custom
    }

    static readonly Dictionary<VolumeType, float> audioVolumes = new()
    {
      { VolumeType.Global, MAX_AUDIO_VOLUME },
      { VolumeType.Sfx,    MAX_AUDIO_VOLUME },
      { VolumeType.Music,  MAX_AUDIO_VOLUME },
      { VolumeType.UI,     MAX_AUDIO_VOLUME },
      { VolumeType.Custom, MAX_AUDIO_VOLUME },
    };

    /// <summary>
    /// Gets the current volume level for the specified audio classType.
    /// </summary>
    /// <returns>The volume level for the specified audio classType.</returns>
    public static float GetVolume(VolumeType volumeType) {
      return audioVolumes[volumeType];
    }

    /// <summary>
    /// Adjusts the volume for the specified audio classType by adding the given value to its current volume.
    /// </summary>
    public static void ModifyVolume(VolumeType volumeType, float volume) {
      SetVolume(volumeType, GetVolume(volumeType) + volume);
    }

    /// <summary>
    /// Sets the volume level for the specified audio classType to the new volume. Auto clamps to the min/max audio volume (<see cref="MIN_AUDIO_VOLUME"/>, <see cref="MAX_AUDIO_VOLUME"/>)
    /// </summary>
    public static void SetVolume(VolumeType volumeType, float volume) {
      audioVolumes[volumeType] = Mathf.Clamp(volume, MIN_AUDIO_VOLUME, MAX_AUDIO_VOLUME);
    }

    /// <summary>
    /// Returns the final volume after applying both the classType volume
    /// and the global volume (unless volumeType is <see cref="VolumeType.Global"/>).
    /// </summary>
    /// <returns>
    /// Proper volume level based on audio classType.
    /// </returns>
    public static float CalculateVolumeBasedOnType(float volume, VolumeType volumeType) {
      float result = volume * GetVolume(volumeType);

      if (volumeType != VolumeType.Global)
        result *= GetVolume(VolumeType.Global);

      return result;
    }

    /// <summary>
    /// Multiplies the specified volume by the current global volume level.
    /// </summary>
    public static float MultiplyByGlobalVolume(float volume) => volume * GetVolume(VolumeType.Global);

    /// <summary>
    /// Calculates the effective playback duration of an audio clip after adjusting for pitch.
    /// </summary>
    /// <returns>Adjusted clip length based on pitch (clipLength / pitch).</returns>
    public static float CalculateClipLength(float clipLength, float pitch) => clipLength / Math.Abs(pitch);

    /// <summary>
    /// Calculates the pitch adjustment factor needed to play an audio clip at a specified duration.
    /// </summary>
    /// <returns>Pitch factor to achieve the desired playback time (clipLength * time).</returns>
    public static float CalculateClipPitchWithLength(float clipLength, float time) => clipLength * time;

    /// <summary>
    /// Calculates the pitch variance to add randomness to playback.
    /// </summary>
    /// <returns>Random number between 1 - pitchVariance and 1 + pitchVariance.</returns>
    public static float CalculatePitchVariance(float pitchVariance) => UnityEngine.Random.Range(MAX_AUDIO_VOLUME - pitchVariance, MAX_AUDIO_VOLUME + pitchVariance);
  }
}