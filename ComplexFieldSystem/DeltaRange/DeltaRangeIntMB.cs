using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class DeltaRangeIntMB : MonoBehaviour, IDeltaRange<int>
    {
        [SerializeField]
        DeltaRangeIntE deltaRangeInt = new DeltaRangeIntE();

        public int Value => ((IModField<int>)deltaRangeInt).Value;

        public int BaseValue { get => ((IModField<int>)deltaRangeInt).BaseValue; set => ((IModField<int>)deltaRangeInt).BaseValue = value; }

        public IDisposable AddChangeMod(Func<int, int> modifier, int priority = 0)
        {
            return ((IDelta<int>)deltaRangeInt).AddChangeMod(modifier, priority);
        }

        public IDisposable AddMod(Func<int, int> modifier, int priority = 0)
        {
            return ((IModifiable<int>)deltaRangeInt).AddMod(modifier, priority);
        }

        public int CalculateWithMods(int sourceValue)
        {
            return ((IModProcessor<int>)deltaRangeInt).CalculateWithMods(sourceValue);
        }


        public void Empty()
        {
            ((IDeltaRange<int>)deltaRangeInt).Empty();
        }

        public void Fill()
        {
            ((IDeltaRange<int>)deltaRangeInt).Fill();
        }

        public IModProcessor<int> GetDecreaseModProcessor()
        {
            return ((IDelta<int>)deltaRangeInt).GetDecreaseModProcessor();
        }

        public float GetFillPercent()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetFillPercent();
        }

        public IModProcessor<int> GetIncreaseModProcessor()
        {
            return ((IDelta<int>)deltaRangeInt).GetIncreaseModProcessor();
        }

        public IModField<int> GetMaxBorderModField()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetMaxBorderModField();
        }

        public int MaxBorder => ((IDeltaRange<int>)deltaRangeInt).MaxBorder;

        public IModField<int> GetMinBorderModField()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetMinBorderModField();
        }

        public int MinBorder => ((IDeltaRange<int>)deltaRangeInt).MinBorder;

        public IObservable<int> GetObsOnChange()
        {
            return ((IDelta<int>)deltaRangeInt).GetObsOnChange();
        }

        public IObservable<int> GetObsOnDecrease()
        {
            return ((IDelta<int>)deltaRangeInt).GetObsOnDecrease();
        }

        public IObservable<int> GetObsOnIncrease()
        {
            return ((IDelta<int>)deltaRangeInt).GetObsOnIncrease();
        }

        public IObservable<int> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetObsOnMaxBorder();
        }

        public IObservable<int> GetObsOnMinBorder()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetObsOnMinBorder();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<int>)deltaRangeInt).GetObsOnModsUpdated();
        }

        public IObservable<int> GetObsOnRangeReached()
        {
            return ((IDeltaRange<int>)deltaRangeInt).GetObsOnRangeReached();
        }

        public int GetValue()
        {
            return ((IFieldGetter<int>)deltaRangeInt).GetValue();
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<int>)deltaRangeInt).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<int>)deltaRangeInt).IsFull();
        }

        public void RemoveAllMods()
        {
            ((IModProcessor<int>)deltaRangeInt).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<int>)deltaRangeInt).RemoveAllModsFromAllModifiables();
        }

        public void SetValue(int value)
        {
            ((IFieldSetter<int>)deltaRangeInt).SetValue(value);
        }

        public IDisposable Subscribe(IObserver<int> observer)
        {
            return ((IObservable<int>)deltaRangeInt).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<int>> observer)
        {
            return ((IObservable<IDeltaRange<int>>)deltaRangeInt).Subscribe(observer);
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<int>)deltaRangeInt).RaiseModsUpdatedEvent();
        }

        int IDelta<int>.Increase(int value)
        {
            return ((IDelta<int>)deltaRangeInt).Increase(value);
        }

        int IDelta<int>.Decrease(int value)
        {
            return ((IDelta<int>)deltaRangeInt).Decrease(value);
        }
    }
}