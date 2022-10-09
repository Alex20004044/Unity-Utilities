using Sirenix.OdinInspector;
using System;
using UniRx;
using UnityEngine;
using TimeMode = MSFD.IRechargable<float>.TimeMode;
namespace MSFD
{
    [Serializable]
    public class RechargableE : IRechargable<float>
    {
        [FoldoutGroup(EditorConstants.debugGroup, 100)]
        [SerializeField]
        DeltaRangeE charge = new DeltaRangeE();
        [FoldoutGroup(EditorConstants.debugGroup, 100)]
        [SerializeField]
        DeltaE rechargeSpeed = new DeltaE(-1);

#if UNITY_EDITOR
        [HorizontalGroup("Stats", Order = 10)]
        [OnStateUpdate("@" + nameof(CalculateRechargeTime) + "()")]
        [ReadOnly]
        [ShowInInspector]
        [Obsolete(EditorConstants.editorOnly)]
        float fullReloadTime;
        [HorizontalGroup("Stats")]
        [ReadOnly]
        [ShowInInspector]
        [Obsolete(EditorConstants.editorOnly)]
        float timeLeft;
#endif
        [HorizontalGroup(EditorConstants.debugGroup + "/TimeSettings")]
        [SerializeField]
        TimeMode timeMode = TimeMode.scaledTime;
        [HorizontalGroup(EditorConstants.debugGroup + "/TimeSettings", LabelWidth = 120)]
        [SerializeField]
        float updateDelay = 0.1f;

        ReactiveProperty<bool> isRechargeStarted = new ReactiveProperty<bool>();

        #region SetShortcut
#if UNITY_EDITOR
        //[FoldoutGroup("Shortcut", -1, Expanded = true)]
        [HorizontalGroup("Base")]
        [Obsolete(EditorConstants.editorOnly)]
        [ShowInInspector]
        float RechargeSpeed { get => rechargeSpeed.BaseValue; set => rechargeSpeed.BaseValue = value; }
        //[FoldoutGroup("Shortcut")]
        [HorizontalGroup("Base")]
        [Obsolete(EditorConstants.editorOnly)]
        [ShowInInspector]
        float Charge { get => charge.BaseValue; set => charge.BaseValue = value; }
        //[FoldoutGroup("Shortcut")]
        [HorizontalGroup("Range")]
        [Obsolete(EditorConstants.editorOnly)]
        [ShowInInspector]
        Vector2 RangeBorders
        {
            get => new Vector2(GetMinBorderModField().BaseValue, charge.GetMaxBorderModField().BaseValue);
            set { GetMinBorderModField().BaseValue = value.x; charge.GetMaxBorderModField().BaseValue = value.y; }
        }
        //[FoldoutGroup("Shortcut")]
        [PropertyOrder(5)]
        [HideLabel]
        [OnStateUpdate("@__progressBar = Value")]
        [ProgressBar("$MinBorder", "$MaxBorder")]
        [ShowInInspector]
        [Obsolete(EditorConstants.editorOnly)]
        float __progressBar;

#endif
        #endregion
        IDisposable disposable;

        public RechargableE(float value = 100, float minBorder = 0, float maxBorder = 100, float rechargeSpeed = -1f,
            TimeMode timeMode = TimeMode.scaledTime, float updateDelay = ComplexFieldValues.updateRate)
        {
            charge = new DeltaRangeE(value, minBorder, maxBorder);
            this.rechargeSpeed.BaseValue = rechargeSpeed;
            this.timeMode = timeMode;
            this.updateDelay = updateDelay;
        }

        ~RechargableE()
        {
            disposable?.Dispose();
        }

        [GUIColor("@" + nameof(GetStartRechargeButtonColor) + "()")]
        [FoldoutGroup(EditorConstants.debugGroup)]
        [HorizontalGroup(EditorConstants.debugGroup + "/RechargeControl")]
        [Button]
        public void StartRecharge()
        {
            isRechargeStarted.Value = true;
            disposable = Observable.Interval(TimeSpan.FromSeconds(updateDelay)).Subscribe((x) => RechargeRoutine());
        }
        [GUIColor("@" + nameof(GetStopRechargeButtonColor) + "()")]
        [FoldoutGroup(EditorConstants.debugGroup)]
        [HorizontalGroup(EditorConstants.debugGroup + "/RechargeControl")]
        [Button]
        public void StopRecharge()
        {
            isRechargeStarted.Value = false;
            disposable.Dispose();
        }

        void RechargeRoutine()
        {
            if (timeMode == TimeMode.scaledTime)
                charge.BaseValue += rechargeSpeed * updateDelay * Time.timeScale;
            else
                charge.BaseValue += rechargeSpeed * updateDelay;
        }

        public IObservable<bool> GetObsIsRechargeStarted()
        {
            return isRechargeStarted;
        }

        public IDelta<float> GetRechargeSpeed()
        {
            return rechargeSpeed;
        }

        public void SetTimeMode(IRechargable<float>.TimeMode timeMode = IRechargable<float>.TimeMode.scaledTime)
        {
            this.timeMode = timeMode;
            /*            StopRecharge();
                        StartRecharge();*/
        }
        #region Stats
#if UNITY_EDITOR
        [Obsolete(EditorConstants.editorOnly)]
        void CalculateRechargeTime()
        {
            fullReloadTime = ((charge.MaxBorder - charge.MinBorder) / rechargeSpeed);
            if (rechargeSpeed >= 0)
            {
                timeLeft = (charge.MaxBorder - charge) / rechargeSpeed;
            }
            else
            {
                timeLeft = (charge.Value - charge.MinBorder) / rechargeSpeed;
            }
        }
#endif
        [Obsolete(EditorConstants.editorOnly)]
        Color GetStartRechargeButtonColor()
        {
            if (isRechargeStarted.Value)
                return EditorConstants.GetGreenColor();
            else
                return EditorConstants.GetStandartColor();
        }
        [Obsolete(EditorConstants.editorOnly)]
        Color GetStopRechargeButtonColor()
        {
            if (!isRechargeStarted.Value && Application.isPlaying)
                return EditorConstants.GetGreenColor();
            else
                return EditorConstants.GetStandartColor();
        }
        #endregion


        #region DeltaRangeValue
        public float Value => charge.Value;

        public float BaseValue { get => charge.BaseValue; set => charge.BaseValue = value; }

        public IDisposable AddChangeMod(Func<float, float> modifier, int priority = 0)
        {
            return charge.AddChangeMod(modifier, priority);
        }

        public IDisposable AddMod(Func<float, float> modifier, int priority = 0)
        {
            return charge.AddMod(modifier, priority);
        }

        public float CalculateWithMods(float sourceValue)
        {
            return charge.CalculateWithMods(sourceValue);
        }

        public float Decrease(float value)
        {
            return charge.Decrease(value);
        }

        public void Empty()
        {
            charge.Empty();
        }

        public void Fill()
        {
            charge.Fill();
        }

        public IModProcessor<float> GetDecreaseModProcessor()
        {
            return charge.GetDecreaseModProcessor();
        }

        public float GetFillPercent()
        {
            return charge.GetFillPercent();
        }

        public IModProcessor<float> GetIncreaseModProcessor()
        {
            return charge.GetIncreaseModProcessor();
        }

        public IModField<float> GetMaxBorderModField()
        {
            return charge.GetMaxBorderModField();
        }

        public float MaxBorder => charge.MaxBorder;

        public IModField<float> GetMinBorderModField()
        {
            return charge.GetMinBorderModField();
        }

        public float MinBorder => charge.MinBorder;

        public IObservable<float> GetObsOnChange()
        {
            return charge.GetObsOnChange();
        }

        public IObservable<float> GetObsOnDecrease()
        {
            return charge.GetObsOnDecrease();
        }

        public IObservable<float> GetObsOnIncrease()
        {
            return charge.GetObsOnIncrease();
        }

        public IObservable<float> GetObsOnMaxBorder()
        {
            return charge.GetObsOnMaxBorder();
        }

        public IObservable<float> GetObsOnMinBorder()
        {
            return charge.GetObsOnMinBorder();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return charge.GetObsOnModsUpdated();
        }

        public IObservable<float> GetObsOnRangeReached()
        {
            return charge.GetObsOnRangeReached();
        }


        public float GetValue()
        {
            return charge.GetValue();
        }

        public float Increase(float value)
        {
            return charge.Increase(value);
        }

        public bool IsEmpty()
        {
            return charge.IsEmpty();
        }

        public bool IsFull()
        {
            return charge.IsFull();
        }

        public void RemoveAllMods()
        {
            charge.RemoveAllMods();
        }

        public void RemoveAllModsFromAllModifiables()
        {
            charge.RemoveAllModsFromAllModifiables();
        }


        public void SetValue(float value)
        {
            charge.SetValue(value);
        }


        public IDisposable Subscribe(IObserver<float> observer)
        {
            return charge.Subscribe(observer);
        }

        public IDisposable Subscribe(IObserver<IDeltaRange<float>> observer)
        {
            return charge.Subscribe(observer);
        }

        public void RaiseModsUpdatedEvent()
        {
            throw new NotImplementedException();
        }
        public bool IsRechargeStarted()
        {
            return isRechargeStarted.Value;
        }
        #endregion
    }
}