using System;
using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  public static class ObjectDelays {
    /// <summary>
    /// Invokes the specified action after waiting for the given amount of time.
    /// </summary>
    /// <param name="action">The action to execute after the delay has elapsed. Cannot be null.</param>
    public static Coroutine Delay(Action action, float time, bool useRealtime = true) {
      return
        CoroutineHelper.Starter.StartCoroutine(DelayCoroutine(action, time, useRealtime));
    }

    public static Coroutine DelayFrame(Action action) {
      return
        CoroutineHelper.Starter.StartCoroutine(DelayFrameCoroutine(action));
    }

    private static IEnumerator DelayCoroutine(Action function, float time, bool useRealtime) {
      yield return useRealtime ? new WaitForSecondsRealtime(time) : new WaitForSeconds(time);
      function?.Invoke();
    }

    private static IEnumerator DelayFrameCoroutine(Action function) {
      yield return null; // Wait for the next frame
      function?.Invoke();
    }
  }
}