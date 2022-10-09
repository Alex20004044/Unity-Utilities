using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public class ModField<T> : IModField<T>
    {
        [LabelWidth(50)]
        [PropertyOrder(-1)]
        [HorizontalGroup]
        [ShowInInspector]
        [ReadOnly]
        public T Value
        {
            get
            {
                return GetValue(); 
            }
        }
        public T BaseValue
        {
            get => baseValue;
            set
            {
                SetValue(value);

            }
        }
        [LabelWidth(75)]
        //[InlineButton("@" + nameof(onValueChanged) + ".OnNext(Value)", "Update")]
        [InlineButton("@" + nameof(RaiseModsUpdatedEvent) + "()", "Update")]
        [HorizontalGroup]
        [SerializeField]
        [Obsolete]
        T baseValue;
        //[HideLabel]
        [InlineProperty()]
        [LabelWidth(125)]
        [PropertyOrder(10)]
        [SerializeField]
        ModProcessor<T> modProcessor = new ModProcessor<T>();

        Subject<T> onValueChanged = new Subject<T>();
        IDisposable disposable;
        public ModField(T value)
        {
            BaseValue = value;
            Initialize();
        }
        public ModField()
        {
            BaseValue = default(T);
            Initialize();
        }
        void Initialize()
        {
            disposable = modProcessor.GetObsOnModsUpdated().Subscribe((x) => onValueChanged.OnNext(Value));
        }
        //Is it neccessary?
        ~ModField()
        {
            disposable.Dispose();
        }

        /// <summary>
        /// GetModifiedValue
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return CalculateWithMods(baseValue);
        }
        /// <summary>
        /// SetBaseValue
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(T value)
        {
            baseValue = value;
            onValueChanged.OnNext(GetValue());
        }

        public IDisposable AddMod(Func<T, T> modifier, int priority = 0)
        {
            return modProcessor.AddMod(modifier, priority);
        }
        public T CalculateWithMods(T sourceValue)
        {
            return modProcessor.CalculateWithMods(sourceValue);
        }
        public void RemoveAllMods()
        {
            modProcessor.RemoveAllMods();
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return modProcessor.GetObsOnModsUpdated();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            observer.OnNext(Value);
            return onValueChanged.Subscribe(observer);
        }

        public void RaiseModsUpdatedEvent()
        {
            modProcessor.RaiseModsUpdatedEvent();
        }

        public static implicit operator T(ModField<T> modField)
        {
            return modField.Value;
        }
    }
}