using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class DeltaRange : DeltaRangeBase<float>
    {
        public DeltaRange(float value = 100, float minBorder = 0, float maxBorder = 100) : base(value, minBorder, maxBorder)
        {
        }

        public override float Decrease(float value)
        {
            var delta = DecreaseMod(value);
            float oldBaseValue = BaseValue;
            BaseValue -= delta;
            return oldBaseValue - BaseValue;
        }
        public override float Increase(float value)
        {
            var delta = IncreaseMod(value);
            float oldBaseValue = BaseValue;
            BaseValue += delta;
            return oldBaseValue - BaseValue;
        }

        public override IObservable<float> GetObsOnRangeReached()
        {
            return Observable.Merge(GetObsOnMaxBorder(), GetObsOnMinBorder().Select((x) => -x));
        }

        public override bool IsEmpty()
        {
            return GetValue() <= minBorder.GetValue();
        }

        public override bool IsFull()
        {
            return GetValue() >= maxBorder.GetValue();
        }

        public override void SetValue(float value)
        {
            var delta = value - baseModField.Value;
            if (delta != 0)
                onValueChanged.OnNext(delta);
            if (value <= minBorder.GetValue())
            {
                baseModField.SetValue(minBorder.GetValue());
                onMinBorderSubject.OnNext(Mathf.Abs(delta));
            }
            else if (value >= maxBorder.GetValue())
            {
                baseModField.SetValue(maxBorder.GetValue());
                onMaxBorderSubject.OnNext(Mathf.Abs(delta));
            }
            else
                baseModField.SetValue(value);
        }
    }
}