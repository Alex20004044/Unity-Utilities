using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class RechargableMB : MonoBehaviour, IRechargable<float>
    {
        [SerializeField]
        bool isStartRechargeOnEnable = true;
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        RechargableE rechargable = new RechargableE();

        private void OnEnable()
        {
            if (isStartRechargeOnEnable)
                rechargable.StartRecharge();
        }
        void OnDisable()
        {
            if (isStartRechargeOnEnable)
                rechargable.StopRecharge();
        }
        public float Value => ((IModField<float>)rechargable).Value;

        public float BaseValue { get => ((IModField<float>)rechargable).BaseValue; set => ((IModField<float>)rechargable).BaseValue = value; }

        public IDisposable AddMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IModifiable<float>)rechargable).AddMod(modifier, priority);
        }

        public IDisposable AddChangeMod(Func<float, float> modifier, int priority = 0)
        {
            return ((IDelta<float>)rechargable).AddChangeMod(modifier, priority);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)rechargable).CalculateWithMods(sourceValue);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)rechargable).Decrease(value);
        }

        public void Empty()
        {
            ((IDeltaRange<float>)rechargable).Empty();
        }

        public void Fill()
        {
            ((IDeltaRange<float>)rechargable).Fill();
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)rechargable).GetDecreaseModProcessor();
        }

        public float GetFillPercent()
        {
            return ((IDeltaRange<float>)rechargable).GetFillPercent();
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)rechargable).GetIncreaseModProcessor();
        }

        public float MaxBorder => ((IDeltaRange<float>)rechargable).MaxBorder;

        public IModField<float> GetMaxBorderModField()
        {
            return ((IDeltaRange<float>)rechargable).GetMaxBorderModField();
        }

        public float MinBorder => ((IDeltaRange<float>)rechargable).MinBorder;

        public IModField<float> GetMinBorderModField()
        {
            return ((IDeltaRange<float>)rechargable).GetMinBorderModField();
        }

        public IObservable<bool> GetObsIsRechargeStarted()
        {
            return ((IRechargable<float>)rechargable).GetObsIsRechargeStarted();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)rechargable).GetObsOnChange();
        }

        public IObservable<float> GetObsOnDecrease()
        {
            return ((IDelta<float>)rechargable).GetObsOnDecrease();
        }

        public IObservable<float> GetObsOnIncrease()
        {
            return ((IDelta<float>)rechargable).GetObsOnIncrease();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<float>)rechargable).GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return ((IDeltaRange<float>)rechargable).GetObsOnMinBorder();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)rechargable).GetObsOnModsUpdated();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return ((IDeltaRange<float>)rechargable).GetObsOnRangeReached();
        }

        public IDelta<float> GetRechargeSpeed()
        {
            return ((IRechargable<float>)rechargable).GetRechargeSpeed();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)rechargable).GetValue();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)rechargable).Increase(value);
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<float>)rechargable).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<float>)rechargable).IsFull();
        }

        public void RemoveAllMods()
        {
            ((IModifiable<float>)rechargable).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)rechargable).RemoveAllModsFromAllModifiables();
        }

        public void SetTimeMode(IRechargable<float>.TimeMode timeMode = IRechargable<float>.TimeMode.scaledTime)
        {
            ((IRechargable<float>)rechargable).SetTimeMode(timeMode);
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)rechargable).SetValue(value);
        }

        public void StartRecharge()
        {
            ((IRechargable<float>)rechargable).StartRecharge();
        }

        public void StopRecharge()
        {
            ((IRechargable<float>)rechargable).StopRecharge();
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)rechargable).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return ((IObservable<IDeltaRange<float>>)rechargable).Subscribe(observer);
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)rechargable).RaiseModsUpdatedEvent();
        }

        public bool IsRechargeStarted()
        {
            return ((ITimer)rechargable).IsRechargeStarted();
        }
    }
}