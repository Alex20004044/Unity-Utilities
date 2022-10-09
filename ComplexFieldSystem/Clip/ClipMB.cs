using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public class ClipMB : MonoBehaviour, IClip<float>
    {
        [SerializeField]
        bool isStartRechargeOnEnable = true;
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        Clip clip = new Clip();

        private void OnEnable()
        {
            if (isStartRechargeOnEnable)
                clip.StartRecharge();
        }
        void OnDisable()
        {
            if (isStartRechargeOnEnable)
                clip.StopRecharge();
        }

        public float MinBorder => ((IDeltaRange<float>)clip).MinBorder;

        public float MaxBorder => ((IDeltaRange<float>)clip).MaxBorder;

        public float Value => ((IModField<float>)clip).Value;

        public float BaseValue { get => ((IModField<float>)clip).BaseValue; set => ((IModField<float>)clip).BaseValue = value; }


        public IRechargable<float> GetAmmo()
        {
            return ((IClip<float>)clip).GetAmmo();
        }

        public IDelta<float> GetShootCost()
        {
            return ((IClip<float>)clip).GetShootCost();
        }

        public IRechargable<float> GetReloading()
        {
            return ((IClip<float>)clip).GetReloading();
        }

        public IDelta<float> GetReloadingTime()
        {
            return ((IClip<float>)clip).GetReloadingTime();
        }

        public bool TrySpecialShoot(float shootCost, float reloadingTime)
        {
            return ((IClip<float>)clip).TrySpecialShoot(shootCost, reloadingTime);
        }

        public bool IsCanSpecialShoot(float shootCost)
        {
            return ((IClip<float>)clip).IsCanSpecialShoot(shootCost);
        }

        public bool IsCanShoot()
        {
            return ((IClip<float>)clip).IsCanShoot();
        }

        public bool TryShoot()
        {
            return ((IClip<float>)clip).TryShoot();
        }

        public IObservable<Unit> GetObsOnCanShoot()
        {
            return ((IClip<float>)clip).GetObsOnCanShoot();
        }

        public IDelta<float> GetRechargeSpeed()
        {
            return ((IRechargable<float>)clip).GetRechargeSpeed();
        }

        public void StopRecharge()
        {
            ((IRechargable<float>)clip).StopRecharge();
        }

        public void StartRecharge()
        {
            ((IRechargable<float>)clip).StartRecharge();
        }

        public IObservable<bool> GetObsIsRechargeStarted()
        {
            return ((IRechargable<float>)clip).GetObsIsRechargeStarted();
        }

        public void SetTimeMode(IRechargable<float>.TimeMode timeMode = IRechargable<float>.TimeMode.scaledTime)
        {
            ((IRechargable<float>)clip).SetTimeMode(timeMode);
        }

        public void Empty()
        {
            ((IDeltaRange<float>)clip).Empty();
        }

        public void Fill()
        {
            ((IDeltaRange<float>)clip).Fill();
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<float>)clip).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<float>)clip).IsFull();
        }

        public IModField<float> GetMinBorderModField()
        {
            return ((IDeltaRange<float>)clip).GetMinBorderModField();
        }

        public IModField<float> GetMaxBorderModField()
        {
            return ((IDeltaRange<float>)clip).GetMaxBorderModField();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return ((IDeltaRange<float>)clip).GetObsOnMinBorder();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<float>)clip).GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return ((IDeltaRange<float>)clip).GetObsOnRangeReached();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)clip).Increase(value);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)clip).Decrease(value);
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)clip).GetIncreaseModProcessor();
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)clip).GetDecreaseModProcessor();
        }

        public IDisposable AddChangeMod(Func<float, float> mod, int priority = 0)
        {
            return ((IDelta<float>)clip).AddChangeMod(mod, priority);
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)clip).RemoveAllModsFromAllModifiables();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)clip).GetObsOnChange();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)clip).GetValue();
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)clip).SetValue(value);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)clip).CalculateWithMods(sourceValue);
        }

        public IDisposable AddMod(Func<float, float> mod, int priority = 0)
        {
            return ((IModifiable<float>)clip).AddMod(mod, priority);
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)clip).GetObsOnModsUpdated();
        }

        public void RemoveAllMods()
        {
            ((IModifiable<float>)clip).RemoveAllMods();
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)clip).RaiseModsUpdatedEvent();
        }

        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)clip).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return ((IObservable<IDeltaRange<float>>)clip).Subscribe(observer);
        }

        public IObservable<Unit> GetObsOnShoot()
        {
            return ((IClip<float>)clip).GetObsOnShoot();
        }

        public bool IsRechargeStarted()
        {
            return ((ITimer)clip).IsRechargeStarted();
        }
    }
}