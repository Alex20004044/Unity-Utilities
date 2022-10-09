using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class DeltaRangeInt : DeltaRangeBase<int>
    {
        public DeltaRangeInt(int value = 100, int minBorder = 0, int maxBorder = 100) : base(value, minBorder, maxBorder)
        {
        }
        public override int Decrease(int value)
        {
            var delta = DecreaseMod(value);
            int oldBaseValue = BaseValue;
            BaseValue -= delta;
            return oldBaseValue - BaseValue;
        }
        public override int Increase(int value)
        {
            var delta = IncreaseMod(value);
            int oldBaseValue = BaseValue;
            BaseValue += delta;
            return oldBaseValue - BaseValue;
        }

        public override IObservable<int> GetObsOnRangeReached()
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

        public override void SetValue(int value)
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