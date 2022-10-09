using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public class Delta : DeltaBase<float>
    {
        public Delta(float value) : base(value)
        {
        }
        public Delta() : base()
        {
        }
        public override float Decrease(float value)
        {
            float delta = DecreaseMod(value);
            BaseValue = baseModField.BaseValue - delta;
            return delta;
        }


        public override float Increase(float value)
        {
            float delta = IncreaseMod(value);
            BaseValue = baseModField.BaseValue + delta;
            return delta;
        }

        public override void SetValue(float value)
        {
            var delta = value - baseModField.Value;
            if (delta != 0)
                onValueChanged.OnNext(delta);

            baseModField.SetValue(value);
        }
    }
}