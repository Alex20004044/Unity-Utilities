using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace MSFD
{
    [Serializable]
    public class DeltaInt : DeltaBase<int>
    {
        public DeltaInt(int value) : base(value)
        {
        }
        public DeltaInt() : base()
        {
        }
        public override int Decrease(int value)
        {
            int delta = DecreaseMod(value);
            BaseValue = baseModField.BaseValue - delta;
            return delta;
        }


        public override int Increase(int value)
        {
            int delta = IncreaseMod(value);
            BaseValue = baseModField.BaseValue + delta;
            return delta;
        }

        public override void SetValue(int value)
        {
            var delta = value - baseModField.Value;
            if (delta != 0)
                onValueChanged.OnNext(delta);
            baseModField.SetValue(value);
        }

    }
}