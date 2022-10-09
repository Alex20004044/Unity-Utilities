using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class DeltaIntMB : MonoBehaviour, IDelta<int>
    {
        [SerializeField]
        DeltaIntE deltaInt = new DeltaIntE();

        public int Value => ((IModField<int>)deltaInt).Value;

        public int BaseValue { get => ((IModField<int>)deltaInt).BaseValue; set => ((IModField<int>)deltaInt).BaseValue = value; }

        public IDisposable AddChangeMod(Func<int, int> modifier, int priority = 0)
        {
            return ((IDelta<int>)deltaInt).AddChangeMod(modifier, priority);
        }

        public IDisposable AddMod(Func<int, int> modifier, int priority = 0)
        {
            return ((IModifiable<int>)deltaInt).AddMod(modifier, priority);
        }

        public int CalculateWithMods(int sourceValue)
        {
            return ((IModProcessor<int>)deltaInt).CalculateWithMods(sourceValue);
        }

        public int Decrease(int value)
        {
            return ((IDelta<int>)deltaInt).Decrease(value);
        }

        public IModProcessor<int> GetDecreaseModProcessor()
        {
            return ((IDelta<int>)deltaInt).GetDecreaseModProcessor();
        }

        public IModProcessor<int> GetIncreaseModProcessor()
        {
            return ((IDelta<int>)deltaInt).GetIncreaseModProcessor();
        }

        public IObservable<int> GetObsOnChange()
        {
            return ((IDelta<int>)deltaInt).GetObsOnChange();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<int>)deltaInt).GetObsOnModsUpdated();
        }

        public int GetValue()
        {
            return ((IFieldGetter<int>)deltaInt).GetValue();
        }

        public int Increase(int value)
        {
            return ((IDelta<int>)deltaInt).Increase(value);
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<int>)deltaInt).RaiseModsUpdatedEvent();
        }

        public void RemoveAllMods()
        {
            ((IModProcessor<int>)deltaInt).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<int>)deltaInt).RemoveAllModsFromAllModifiables();
        }

        public void SetValue(int value)
        {
            ((IFieldSetter<int>)deltaInt).SetValue(value);
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            return ((IObservable<int>)deltaInt).Subscribe(observer);
        }
    }
}