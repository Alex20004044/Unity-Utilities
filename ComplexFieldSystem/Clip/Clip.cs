using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public class Clip : IClip<float>, IRechargable<float>
    {
        [SerializeField]
        Rechargable ammo = new Rechargable();
        [SerializeField]
        Delta shootCost = new Delta();
        [SerializeField]
        Rechargable reloading = new Rechargable();
        [SerializeField]
        Delta reloadingTime = new Delta();

        Subject<Unit> onShoot = new Subject<Unit>();
        Subject<Unit> onCanShoot = new Subject<Unit>();
        public Clip()
        {
            onShoot.Subscribe((x) =>
            {
                if (!IsCanShoot())
                {
                    isCanShootInvoked = false;
                }
            });

            Observable.Merge(((IObservable<float>)reloading).Where((x) => x <= 0), 
                ((IObservable<float>)ammo).Where(x => x >= shootCost)).Subscribe((_)=>CheckIsCanShootInvokedWhenReloading());
        }

        bool isCanShootInvoked = false;
        #region Ammo Rechargable
        public float MinBorder => ((IDeltaRange<float>)ammo).MinBorder;

        public float MaxBorder => ((IDeltaRange<float>)ammo).MaxBorder;

        public float Value => ((IModField<float>)ammo).Value;

        public float BaseValue { get => ((IModField<float>)ammo).BaseValue; set => ((IModField<float>)ammo).BaseValue = value; }

        public IDisposable AddChangeMod(Func<float, float> mod, int priority = 0)
        {
            return ((IDelta<float>)ammo).AddChangeMod(mod, priority);
        }

        public IDisposable AddMod(Func<float, float> mod, int priority = 0)
        {
            return ((IModifiable<float>)ammo).AddMod(mod, priority);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return ((IModProcessor<float>)ammo).CalculateWithMods(sourceValue);
        }

        public float Decrease(float value)
        {
            return ((IDelta<float>)ammo).Decrease(value);
        }





        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return ((IDelta<float>)ammo).GetDecreaseModProcessor();
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return ((IDelta<float>)ammo).GetIncreaseModProcessor();
        }

        public IModField<float> GetMaxBorderModField()
        {
            return ((IDeltaRange<float>)ammo).GetMaxBorderModField();
        }

        public IModField<float> GetMinBorderModField()
        {
            return ((IDeltaRange<float>)ammo).GetMinBorderModField();
        }

        public IObservable<bool> GetObsIsRechargeStarted()
        {
            return ((IRechargable<float>)ammo).GetObsIsRechargeStarted();
        }

        public IObservable<float> GetObsOnChange()
        {
            return ((IDelta<float>)ammo).GetObsOnChange();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return ((IDeltaRange<float>)ammo).GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return ((IDeltaRange<float>)ammo).GetObsOnMinBorder();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return ((IModifiable<float>)ammo).GetObsOnModsUpdated();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return ((IDeltaRange<float>)ammo).GetObsOnRangeReached();
        }

        public IDelta<float> GetRechargeSpeed()
        {
            return ((IRechargable<float>)ammo).GetRechargeSpeed();
        }

        public float GetValue()
        {
            return ((IFieldGetter<float>)ammo).GetValue();
        }

        public float Increase(float value)
        {
            return ((IDelta<float>)ammo).Increase(value);
        }

        public bool IsEmpty()
        {
            return ((IDeltaRange<float>)ammo).IsEmpty();
        }

        public bool IsFull()
        {
            return ((IDeltaRange<float>)ammo).IsFull();
        }

        public void RaiseModsUpdatedEvent()
        {
            ((IModifiable<float>)ammo).RaiseModsUpdatedEvent();
        }

        public void RemoveAllMods()
        {
            ((IModifiable<float>)ammo).RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            ((IDelta<float>)ammo).RemoveAllModsFromAllModifiables();
        }

        public void SetTimeMode(IRechargable<float>.TimeMode timeMode = IRechargable<float>.TimeMode.scaledTime)
        {
            ((IRechargable<float>)ammo).SetTimeMode(timeMode);
        }

        public void SetValue(float value)
        {
            ((IFieldSetter<float>)ammo).SetValue(value);
        }



        public IDisposable Subscribe(IObserver<float> observer)
        {
            return ((IObservable<float>)ammo).Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return ((IObservable<IDeltaRange<float>>)ammo).Subscribe(observer);
        }
        #endregion

        //Empty => ammo = 0, reloading = 0
        //Fill => ammo = 100%, reloading = reloadTime
        //Increase => ammo.Increase()
        //Decrease => ammo.Decrease()
        public void Fill()
        {
            ((IDeltaRange<float>)ammo).Fill();
            reloading.Empty();
        }
        public void Empty()
        {
            ((IDeltaRange<float>)ammo).Empty();
            reloading.SetValue(reloadingTime);
        }

        public void StartRecharge()
        {
            ((IRechargable<float>)ammo).StartRecharge();
            reloading.StartRecharge();
        }

        public void StopRecharge()
        {
            ((IRechargable<float>)ammo).StopRecharge();
            reloading.StopRecharge();
        }

        public IRechargable<float> GetAmmo()
        {
            return ammo;
        }
        public IDelta<float> GetShootCost()
        {
            return shootCost;
        }
        public IRechargable<float> GetReloading()
        {
            return reloading;
        }
        public IDelta<float> GetReloadingTime()
        {
            return reloadingTime;
        }
        public bool IsRechargeStarted()
        {
            return reloading.IsRechargeStarted();
        }
        #region Shoot
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public bool TrySpecialShoot(float shootCost, float reloadingTime)
        {
            if (IsCanSpecialShoot(shootCost))
            {
                ammo.Decrease(shootCost);
                reloading.Increase(reloadingTime);
                onShoot.OnNext(Unit.Default);
                return true;
            }
            else
                return false;
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public bool IsCanSpecialShoot(float shootCost)
        {
            return ammo >= shootCost && reloading <= 0;
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public bool IsCanShoot()
        {
            return IsCanSpecialShoot(shootCost.Value);
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public bool TryShoot()
        {
            return TrySpecialShoot(shootCost, reloadingTime);
        }
        public IObservable<Unit> GetObsOnCanShoot()
        {
            return onCanShoot;
        }

        void CheckIsCanShootInvokedWhenReloading()
        {
            if (isCanShootInvoked)
                return;
            else if (IsCanShoot())
            {
                isCanShootInvoked = true;
                onCanShoot.OnNext(Unit.Default);
            }
        }

        public IObservable<Unit> GetObsOnShoot()
        {
            return onShoot;
        }


        #endregion
    }
}