using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Cameras {
  public static class CameraShake {
    /// Default curve used for screenshake
    public static AnimationCurve defaultScreenshakeCurve = new(
        new Keyframe(0, 0),
        new Keyframe(0.2f, 0.1f, -0.05f, -0.05f),
        new Keyframe(1, 0)
    );

    /// Dictionary of base camera positions to reference
    public static Dictionary<Transform, Vector3> cameraBasePositions = new();

    /// Dictionary of coroutines that screenshaking cameras are using
    public static Dictionary<Transform, IEnumerator> cameraCoroutines = new();

    /// <summary>
    /// Shakes the inputted camera for a specified amount of time with a certain intensity following an animation curve
    /// </summary>
    /// <remarks>For best results, don't move the camera directly via any other scripts. Camera can also be any transform, so you can shake other objects with these</remarks>
    /// <param name="camera">Camera to apply screenshake to, default is Camera.main</param>
    /// <param name="intensity">A multiplier for more screenshake</param>
    /// <param name="duration">How many seconds the screenshake lasts for</param>
    /// <param name="curve">Curve used to apply screenshake</param>
    /// <param name="useRealtime">If true, will use Time.unscaledDeltatime, instead of Time.deltaTime</param>
    public static void Screenshake(Transform camera = default, float intensity = 1, float duration = 0.5f, AnimationCurve curve = default, bool useRealtime = true) {
      if (curve == default)
        curve = defaultScreenshakeCurve;
      if (camera == default)
        camera = Camera.main.transform;

      if (cameraCoroutines.ContainsKey(camera))
        StopScreenshake(camera);

      cameraCoroutines.Add(camera, ScreenshakeCoroutine(camera, intensity, duration, curve, useRealtime));

      CoroutineHelper.Starter.StartCoroutine(cameraCoroutines[camera]);
    }
    private static IEnumerator ScreenshakeCoroutine(Transform camera, float intensity, float duration, AnimationCurve curve, bool useRealtime) {
      Vector3 startPosition = camera.localPosition;

      if (!cameraBasePositions.ContainsKey(camera))
        cameraBasePositions.Add(camera, startPosition);

      float elapsedTime = 0;

      while (elapsedTime < duration) {
        elapsedTime += useRealtime ? Time.unscaledDeltaTime : Time.deltaTime;
        float strength = curve.Evaluate(elapsedTime / duration) * intensity;
        camera.localPosition = startPosition + Random.insideUnitSphere * strength;
        yield return null;
      }

      camera.localPosition = startPosition;
      StopScreenshake(camera);
    }

    /// <summary>
    /// Will stop the screenshake on the inputted camera. If the camera is not shaking a warning will be printed
    /// </summary>
    /// <param name="camera">Camera to stop screenshaking</param>
    public static void StopScreenshake(Transform camera) {
      if (!IsScreenshaking(camera)) {
        Debug.LogWarning("Camera is not screenshaking");
        return;
      }

      CoroutineHelper.Starter.StopCoroutine(cameraCoroutines[camera]);

      camera.localPosition = cameraBasePositions[camera];

      cameraCoroutines.Remove(camera);
      cameraBasePositions.Remove(camera);
    }

    /// <summary>
    /// Returns true if the inputtted camera is currently screenshaking
    /// </summary>
    /// <param name="camera">Camera to check for screenshake</param>
    public static bool IsScreenshaking(Transform camera) {
      if (cameraCoroutines.ContainsKey(camera) && cameraBasePositions.ContainsKey(camera))
        return true;
      else
        return false;
    }
  }
}
