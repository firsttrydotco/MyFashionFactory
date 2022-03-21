﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using MoreMountains.Feedbacks;

namespace MoreMountains.FeedbacksForThirdParty
{
    /// <summary>
    /// Add this class to a Camera with a URP color adjustments post processing and it'll be able to "shake" its values by getting events
    /// </summary>
    [RequireComponent(typeof(Volume))]
    [AddComponentMenu("More Mountains/Feedbacks/Shakers/PostProcessing/MMColorAdjustmentsShaker_URP")]
    public class MMColorAdjustmentsShaker_URP : MMShaker
    {
        /// whether or not to add to the initial value
        public bool RelativeValues = true;

        [Header("Post Exposure")]
        /// the curve used to animate the focus distance value on
        public AnimationCurve ShakePostExposure = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to
        public float RemapPostExposureZero = 0f;
        /// the value to remap the curve's 1 to
        public float RemapPostExposureOne = 1f;

        [Header("Hue Shift")]
        /// the curve used to animate the aperture value on
        public AnimationCurve ShakeHueShift = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to
        [Range(-180f, 180f)]
        public float RemapHueShiftZero = 0f;
        /// the value to remap the curve's 1 to
        [Range(-180f, 180f)]
        public float RemapHueShiftOne = 180f;

        [Header("Saturation")]
        /// the curve used to animate the focal length value on
        public AnimationCurve ShakeSaturation = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to
        [Range(-100f, 100f)]
        public float RemapSaturationZero = 0f;
        /// the value to remap the curve's 1 to
        [Range(-100f, 100f)]
        public float RemapSaturationOne = 100f;

        [Header("Contrast")]
        /// the curve used to animate the focal length value on
        public AnimationCurve ShakeContrast = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        /// the value to remap the curve's 0 to
        [Range(-100f, 100f)]
        public float RemapContrastZero = 0f;
        /// the value to remap the curve's 1 to
        [Range(-100f, 100f)]
        public float RemapContrastOne = 100f;

        protected Volume _volume;
        protected ColorAdjustments _colorAdjustments;

        protected float _initialPostExposure;
        protected float _initialHueShift;
        protected float _initialSaturation;
        protected float _initialContrast;

        protected float _originalShakeDuration;
        protected bool _originalRelativeValues;
        protected AnimationCurve _originalShakePostExposure;
        protected float _originalRemapPostExposureZero;
        protected float _originalRemapPostExposureOne;
        protected AnimationCurve _originalShakeHueShift;
        protected float _originalRemapHueShiftZero;
        protected float _originalRemapHueShiftOne;
        protected AnimationCurve _originalShakeSaturation;
        protected float _originalRemapSaturationZero;
        protected float _originalRemapSaturationOne;
        protected AnimationCurve _originalShakeContrast;
        protected float _originalRemapContrastZero;
        protected float _originalRemapContrastOne;

        /// <summary>
        /// On init we initialize our values
        /// </summary>
        protected override void Initialization()
        {
            base.Initialization();
            _volume = this.gameObject.GetComponent<Volume>();
            _volume.profile.TryGet(out _colorAdjustments);
        }

        /// <summary>
        /// When that shaker gets added, we initialize its shake duration
        /// </summary>
        protected virtual void Reset()
        {
            ShakeDuration = 0.8f;
        }

        /// <summary>
        /// Shakes values over time
        /// </summary>
        protected override void Shake()
        {
            float newPostExposure = ShakeFloat(ShakePostExposure, RemapPostExposureZero, RemapPostExposureOne, RelativeValues, _initialPostExposure);
            _colorAdjustments.postExposure.Override(newPostExposure);
            float newHueShift = ShakeFloat(ShakeHueShift, RemapHueShiftZero, RemapHueShiftOne, RelativeValues, _initialHueShift);
            _colorAdjustments.hueShift.Override(newHueShift);
            float newSaturation = ShakeFloat(ShakeSaturation, RemapSaturationZero, RemapSaturationOne, RelativeValues, _initialSaturation);
            _colorAdjustments.saturation.Override(newSaturation);
            float newContrast = ShakeFloat(ShakeContrast, RemapContrastZero, RemapContrastOne, RelativeValues, _initialContrast);
            _colorAdjustments.contrast.Override(newContrast);
        }

        /// <summary>
        /// Collects initial values on the target
        /// </summary>
        protected override void GrabInitialValues()
        {
            _initialPostExposure = _colorAdjustments.postExposure.value;
            _initialHueShift = _colorAdjustments.hueShift.value;
            _initialSaturation = _colorAdjustments.saturation.value;
            _initialContrast = _colorAdjustments.contrast.value;
        }

        /// <summary>
        /// When we get the appropriate event, we trigger a shake
        /// </summary>
        /// <param name="intensity"></param>
        /// <param name="duration"></param>
        /// <param name="amplitude"></param>
        /// <param name="relativeIntensity"></param>
        /// <param name="attenuation"></param>
        /// <param name="channel"></param>
        public virtual void OnMMColorGradingShakeEvent(AnimationCurve shakePostExposure, float remapPostExposureZero, float remapPostExposureOne,
            AnimationCurve shakeHueShift, float remapHueShiftZero, float remapHueShiftOne,
            AnimationCurve shakeSaturation, float remapSaturationZero, float remapSaturationOne,
            AnimationCurve shakeContrast, float remapContrastZero, float remapContrastOne,
            float duration, bool relativeValues = false,
            float attenuation = 1.0f, int channel = 0, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true)
        {
            if (!CheckEventAllowed(channel) || Shaking)
            {
                return;
            }

            _resetShakerValuesAfterShake = resetShakerValuesAfterShake;
            _resetTargetValuesAfterShake = resetTargetValuesAfterShake;

            if (resetShakerValuesAfterShake)
            {
                _originalShakeDuration = ShakeDuration;
                _originalRelativeValues = RelativeValues;
                _originalShakePostExposure = ShakePostExposure;
                _originalRemapPostExposureZero = RemapPostExposureZero;
                _originalRemapPostExposureOne = RemapPostExposureOne;
                _originalShakeHueShift = ShakeHueShift;
                _originalRemapHueShiftZero = RemapHueShiftZero;
                _originalRemapHueShiftOne = RemapHueShiftOne;
                _originalShakeSaturation = ShakeSaturation;
                _originalRemapSaturationZero = RemapSaturationZero;
                _originalRemapSaturationOne = RemapSaturationOne;
                _originalShakeContrast = ShakeContrast;
                _originalRemapContrastZero = RemapContrastZero;
                _originalRemapContrastOne = RemapContrastOne;
            }

            ShakeDuration = duration;
            RelativeValues = relativeValues;
            ShakePostExposure = shakePostExposure;
            RemapPostExposureZero = remapPostExposureZero;
            RemapPostExposureOne = remapPostExposureOne;
            ShakeHueShift = shakeHueShift;
            RemapHueShiftZero = remapHueShiftZero;
            RemapHueShiftOne = remapHueShiftOne;
            ShakeSaturation = shakeSaturation;
            RemapSaturationZero = remapSaturationZero;
            RemapSaturationOne = remapSaturationOne;
            ShakeContrast = shakeContrast;
            RemapContrastZero = remapContrastZero;
            RemapContrastOne = remapContrastOne;

            Play();
        }

        /// <summary>
        /// Resets the target's values
        /// </summary>
        protected override void ResetTargetValues()
        {
            base.ResetTargetValues();
            _colorAdjustments.postExposure.Override(_initialPostExposure);
            _colorAdjustments.hueShift.Override(_initialHueShift);
            _colorAdjustments.saturation.Override(_initialSaturation);
            _colorAdjustments.contrast.Override(_initialContrast);
        }

        /// <summary>
        /// Resets the shaker's values
        /// </summary>
        protected override void ResetShakerValues()
        {
            base.ResetShakerValues();
            ShakeDuration = _originalShakeDuration;
            RelativeValues = _originalRelativeValues;
            ShakePostExposure = _originalShakePostExposure;
            RemapPostExposureZero = _originalRemapPostExposureZero;
            RemapPostExposureOne = _originalRemapPostExposureOne;
            ShakeHueShift = _originalShakeHueShift;
            RemapHueShiftZero = _originalRemapHueShiftZero;
            RemapHueShiftOne = _originalRemapHueShiftOne;
            ShakeSaturation = _originalShakeSaturation;
            RemapSaturationZero = _originalRemapSaturationZero;
            RemapSaturationOne = _originalRemapSaturationOne;
            ShakeContrast = _originalShakeContrast;
            RemapContrastZero = _originalRemapContrastZero;
            RemapContrastOne = _originalRemapContrastOne;
        }

        /// <summary>
        /// Starts listening for events
        /// </summary>
        public override void StartListening()
        {
            base.StartListening();
            MMColorAdjustmentsShakeEvent_URP.Register(OnMMColorGradingShakeEvent);
        }

        /// <summary>
        /// Stops listening for events
        /// </summary>
        public override void StopListening()
        {
            base.StopListening();
            MMColorAdjustmentsShakeEvent_URP.Unregister(OnMMColorGradingShakeEvent);
        }
    }

    /// <summary>
    /// An event used to trigger vignette shakes
    /// </summary>
    public struct MMColorAdjustmentsShakeEvent_URP
    {
        public delegate void Delegate(AnimationCurve shakePostExposure, float remapPostExposureZero, float remapPostExposureOne,
            AnimationCurve shakeHueShift, float remapHueShiftZero, float remapHueShiftOne,
            AnimationCurve shakeSaturation, float remapSaturationZero, float remapSaturationOne,
            AnimationCurve shakeContrast, float remapContrastZero, float remapContrastOne,
            float duration, bool relativeValues = false,
            float attenuation = 1.0f, int channel = 0, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true);
        static private event Delegate OnEvent;

        static public void Register(Delegate callback)
        {
            OnEvent += callback;
        }

        static public void Unregister(Delegate callback)
        {
            OnEvent -= callback;
        }

        static public void Trigger(AnimationCurve shakePostExposure, float remapPostExposureZero, float remapPostExposureOne,
            AnimationCurve shakeHueShift, float remapHueShiftZero, float remapHueShiftOne,
            AnimationCurve shakeSaturation, float remapSaturationZero, float remapSaturationOne,
            AnimationCurve shakeContrast, float remapContrastZero, float remapContrastOne,
            float duration, bool relativeValues = false,
            float attenuation = 1.0f, int channel = 0, bool resetShakerValuesAfterShake = true, bool resetTargetValuesAfterShake = true)
        {
            OnEvent?.Invoke(shakePostExposure, remapPostExposureZero, remapPostExposureOne,
                shakeHueShift, remapHueShiftZero, remapHueShiftOne,
                shakeSaturation, remapSaturationZero, remapSaturationOne,
                shakeContrast, remapContrastZero, remapContrastOne,
                duration, relativeValues, attenuation, channel, resetShakerValuesAfterShake, resetTargetValuesAfterShake);
        }
    }
}
