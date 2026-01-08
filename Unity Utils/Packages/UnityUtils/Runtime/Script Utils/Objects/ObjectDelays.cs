using System;
using System.Collections;
using UnityEngine;
using UnityUtils.ScriptUtils;

namespace UnityUtils.ScriptUtils.Objects
{
    public static class ObjectDelays
    {
        /// <summary>
        /// Flips the <see cref="inputBool"/> after the specified amount of time
        /// </summary>
        /// <param name="inputBool">Bool to flip.</param>
        /// <param name="time">Time to wait.</param>
        /// <param name="useRealtime">true to use unscaled real time for the bool (ignoring time scale)</param>
        public static void FlipBoolAfterTime(bool inputBool, float time, bool useRealtime = false)
        {
            ChangeValueAfterTime<bool>(value => inputBool = value, !inputBool, time, useRealtime);
        }

        /// <summary>
        /// Destroys an object after the given amount of time in unscaled time
        /// </summary> 
        public static void DestroyUnscaledtime(GameObject obj, float time)
        {
            CallFunctionAfterTime(() => UnityEngine.Object.Destroy(obj), time, true);
        }

        /// <summary>
        /// Invokes the specified function after waiting for the given amount of time.
        /// </summary>
        /// <param name="function">The action to execute after the delay has elapsed. Cannot be null.</param>
        public static void CallFunctionAfterTime(Action function, float time, bool useRealtime = true)
        {
            CoroutineHelper.Starter.StartCoroutine(CallFunctionAfterTimeCoroutine(function, time, useRealtime));
        }

        /// <summary>
        /// Invokes the specified action with a new value after a delay, optionally using real time or game time for the
        /// delay calculation.
        /// </summary>
        /// <param name="onValueChange">The action to invoke with the updated value after the specified delay.</param>
        /// <param name="updatedValue">The value to pass to the action when the delay has elapsed.</param>
        public static void ChangeValueAfterTime<T>(Action<T> onValueChange, T updatedValue, float time, bool useRealtime)
        {
            CoroutineHelper.Starter.StartCoroutine(ChangeValueAfterTimeCoroutine(onValueChange, updatedValue, time, useRealtime));
        }

        private static IEnumerator CallFunctionAfterTimeCoroutine(Action function, float time, bool useRealtime)
        {
            yield return useRealtime ? new WaitForSecondsRealtime(time) : new WaitForSeconds(time);
            function?.Invoke();
        }

        private static IEnumerator ChangeValueAfterTimeCoroutine<T>(Action<T> onValueChange, T updatedValue, float time, bool useRealtime)
        {
            yield return useRealtime ? new WaitForSecondsRealtime(time) : new WaitForSeconds(time);
            onValueChange?.Invoke(updatedValue);
        }
    }
}