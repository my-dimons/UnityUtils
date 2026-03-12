using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityUtils.ScriptUtils.Objects;

namespace UnityUtils.ScriptUtils.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonHoverPosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Adjustable Values")]
        /// When hovered this is the size the button will be set to.
        public Vector3 hoverLocalPosition;

        /// The amount of seconds that the button will size up or down in.
        public float sizeAnimationSeconds = 0.1f;

        [Space(8)]

        /// true to use unscaled real time for the animation (ignoring time scale)
        public bool useRealtime = true;

        /// The <see cref="AnimationCurve"/> that the button will follow.
        public AnimationCurve SizingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Applied Transform")]

        /// The object to apply the transform to (Default's to the applied object).
        public Transform applyTransform;

        [Header("Debug Values")]

        /// True if the button is being hovered
        public bool hoveringOverButton;

        [Header("Debug Logs")]

        /// Log on any scaling
        public bool logScale;
        /// Log first scale to set scale.
        public bool logScaleUp;
        /// Log second rotate back to default pos.
        public bool logScaleDown;

        Vector3 originalPosition;
        Vector3 hoverPositionVector;

        // Starter is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            originalPosition = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            hoverPositionVector = transform.localPosition + hoverLocalPosition;

            // Stops choppy animation when spam hovering the button
            if (!hoveringOverButton && transform.localPosition == hoverPositionVector)
            {
                ExitHoverAnimation();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (transform.localPosition == originalPosition)
                EnterHoverAnimation();

            hoveringOverButton = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (transform.localPosition == hoverPositionVector)
                ExitHoverAnimation();

            hoveringOverButton = false;
        }

        /// <summary>
        /// Moves the button to the set position: (<see cref="hoverPositionVector"/>).
        /// </summary>
        void EnterHoverAnimation()
        {
            ObjectAnimations.AnimateTransformPosition(applyTransform, originalPosition, hoverPositionVector, sizeAnimationSeconds, useRealtime, SizingCurve);

            if (logScaleUp)
                Debug.Log("Moving button to position");

            DebugRotate();
        }

        /// <summary>
        /// Moves the button to its original position.
        /// </summary>
        void ExitHoverAnimation()
        {
            ObjectAnimations.AnimateTransformPosition(applyTransform, hoverPositionVector, originalPosition, sizeAnimationSeconds, useRealtime, SizingCurve);

            if (logScaleDown)
                Debug.Log("Moving button back");

            DebugRotate();
        }

        private void DebugRotate()
        {
            if (logScale)
                Debug.Log("Moved button");
        }

        void Reset()
        {
            applyTransform = gameObject.transform;
        }
    }
}
