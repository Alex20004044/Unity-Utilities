using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class DeltaMB : MonoBehaviour, IDelta<float>
    {
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        DeltaE deltaFloat = new DeltaE();

        public float Value => ((IModField<float>)deltaFloat).Value;

        public float BaseValue { get => ((IModField<float>)deltaFloat).BaseValue; set => ((IModField<float>)deltaFloat).BaseValue = value; }

        public IDisposable AddChangeMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IDelta<float>)deltaFloat).AddChangeMod(modifier, priority);
        }

        public IDisposable AddMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IModifiable<float>)deltaFloat).AddMod(modifier, priority);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)deltaFloat).CalculateWithMods(sourceValue);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)deltaFloat).Decrease(value);
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)deltaFloat).GetDecreaseModProcessor();
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)deltaFloat).GetIncreaseModProcessor();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)deltaFloat).GetObsOnChange();
        }
        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)deltaFloat).GetObsOnModsUpdated();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)deltaFloat).GetValue();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)deltaFloat).Increase(value);
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)deltaFloat).RaiseModsUpdatedEvent();
        }

        public void RemoveAllMods()
        {
            ((IModProcessor<float>)deltaFloat).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)deltaFloat).RemoveAllModsFromAllModifiables();
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)deltaFloat).SetValue(value);
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)deltaFloat).Subscribe(observer);
        }
    }
}