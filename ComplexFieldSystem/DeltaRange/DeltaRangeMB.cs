using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class DeltaRangeMB : MonoBehaviour, IDeltaRange<float>
    {
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        DeltaRangeE deltaRangeFloat = new DeltaRangeE();

        public float MinBorder => ((IDeltaRange<float>)deltaRangeFloat).MinBorder;

        public float MaxBorder => ((IDeltaRange<float>)deltaRangeFloat).MaxBorder;

        public float Value => ((IModField<float>)deltaRangeFloat).Value;

        public float BaseValue { get => ((IModField<float>)deltaRangeFloat).BaseValue; set => ((IModField<float>)deltaRangeFloat).BaseValue = value; }

        public IDisposable AddChangeMod(Func<float, float> mod, int priority = 0)
        {
            return ((IDelta<float>)deltaRangeFloat).AddChangeMod(mod, priority);
        }

        public IDisposable AddMod(Func<float, float> mod, int priority = 0)
        {
            return ((IModifiable<float>)deltaRangeFloat).AddMod(mod, priority);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)deltaRangeFloat).CalculateWithMods(sourceValue);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)deltaRangeFloat).Decrease(value);
        }

        public void Empty()
        {
            ((IDeltaRange<float>)deltaRangeFloat).Empty();
        }

        public void Fill()
        {
            ((IDeltaRange<float>)deltaRangeFloat).Fill();
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)deltaRangeFloat).GetDecreaseModProcessor();
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)deltaRangeFloat).GetIncreaseModProcessor();
        }

        public IModField<float> GetMaxBorderModField()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).GetMaxBorderModField();
        }

        public IModField<float> GetMinBorderModField()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).GetMinBorderModField();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)deltaRangeFloat).GetObsOnChange();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).GetObsOnMinBorder();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)deltaRangeFloat).GetObsOnModsUpdated();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).GetObsOnRangeReached();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)deltaRangeFloat).GetValue();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)deltaRangeFloat).Increase(value);
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<float>)deltaRangeFloat).IsFull();
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)deltaRangeFloat).RaiseModsUpdatedEvent();
        }

        public void RemoveAllMods()
        {
            ((IModifiable<float>)deltaRangeFloat).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)deltaRangeFloat).RemoveAllModsFromAllModifiables();
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)deltaRangeFloat).SetValue(value);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)deltaRangeFloat).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return ((IObservable<IDeltaRange<float>>)deltaRangeFloat).Subscribe(observer);
        }
    }

}