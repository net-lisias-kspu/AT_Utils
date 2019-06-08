﻿//   Fields.cs
//
//  Author:
//       Allis Tauri <allista@gmail.com>
//
//  Copyright (c) 2019 Allis Tauri

using UnityEngine;

namespace AT_Utils.UI
{
    public abstract class FloatValueUI : MonoBehaviour
    {
        public RectTransform panel;
        public void SetActive(bool enable) => panel.gameObject.SetActive(enable);

        public abstract bool SetValueWithoutNotify(float newValue);
        public abstract FloatEvent onValueChanged { get; }

        protected void changeValueAndNotify(float newValue)
        {
            if(SetValueWithoutNotify(newValue))
                onValueChanged.Invoke(value);
        }

        protected float value;
        public float Value { get => value; set => changeValueAndNotify(value); }
    }

    public abstract class BoundedFloatValueUI : FloatValueUI
    {
        public abstract float Min { get; set; }
        public abstract float Max { get; set; }
        public float Range => Max - Min;

        protected virtual float clampValue(float newValue)
        {
            if(newValue > Max)
                return Max;
            if(newValue < Min)
                return Min;
            return newValue;
        }
    }

    public abstract class RingBoundedFloatValueUI : BoundedFloatValueUI
    {
        public bool ring;

        protected override float clampValue(float newValue)
        {
            if(ring)
            {
                if(newValue > Max)
                    return newValue % Max + Min;
                if(newValue < Min)
                    return Max - Min + newValue;
                return newValue;
            }
            return base.clampValue(newValue);
        }
    }
}
