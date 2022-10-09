using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace MSFD
{
    [Serializable]
    public abstract class DeltaBase<T> : IDelta<T>
    {
        public T Value => GetValue();

        public T BaseValue
        {
            get
            {
                return baseModField.BaseValue;
            }
            set
            {
                SetValue(value);
            }
        }


        [HorizontalGroup]
        [HideLabel]
        [InlineProperty]
        [SerializeField]
        [Obsolete]
        protected ModField<T> baseModField = new ModField<T>();
        protected Subject<T> onValueChanged = new Subject<T>();

        [FoldoutGroup(EditorConstants.debugGroup)]
        [ShowInInspector]
        protected ModProcessor<T> increaseModProc = new ModProcessor<T>();
        [FoldoutGroup(EditorConstants.debugGroup)]
        [ShowInInspector]
        protected ModProcessor<T> decreaseModProc = new ModProcessor<T>();


        public DeltaBase(T value)
        {
            baseModField.BaseValue = value;
        }
        public DeltaBase()
        {
            baseModField.BaseValue = default(T);
        }

        #region Base
        public IDisposable Subscribe(IObserver<T> observer)
        {
            return baseModField.Subscribe(observer);
        }
        public virtual IDisposable AddMod(Func<T, T> modifier, int priority = 0)
        {
            return baseModField.AddMod(modifier, priority);
        }
        public void RemoveAllMods()
        {
            baseModField.RemoveAllMods();
        }
        public void RaiseModsUpdatedEvent()
        {
            baseModField.RaiseModsUpdatedEvent();
        }
        public T CalculateWithMods(T sourceValue)
        {
            return baseModField.CalculateWithMods(sourceValue);
        }
        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return baseModField.GetObsOnModsUpdated();
        }
        #endregion
        #region Increase/Decrease

        protected T IncreaseMod(T value)
        {
            return increaseModProc.CalculateWithMods(value);
        }

        protected T DecreaseMod(T value)
        {
            return decreaseModProc.CalculateWithMods(value);
        }
        public IDisposable AddChangeMod(Func<T, T> mod, int priority = 0)
        {
            CompositeDisposable disposables = new CompositeDisposable();
            disposables.Add(increaseModProc.AddMod(mod, priority));
            disposables.Add(decreaseModProc.AddMod(mod, priority));
            return disposables;
        }
        public IModProcessor<T> GetIncreaseModProcessor()
        {
            return increaseModProc;
        }
        public IModProcessor<T> GetDecreaseModProcessor()
        {
            return decreaseModProc;
        }
        [FoldoutGroup(EditorConstants.debugGroup, order: 100)]
        [Button]
        /// <summary>
        /// T delta = IncreaseModifier(value);
        /// baseValue.Value += delta;
        /// onIncreaseSubject.OnNext(delta);
        /// </summary>
        /// <param name="value"></param>
        public abstract T Increase(T value);
        /*{
            T delta = IncreaseModifier(value);
            BaseValue = baseValue.Value + delta;
        }*/
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public abstract T Decrease(T value);
        /*{
            T delta = DecreaseModifier(value);
            BaseValue = baseValue.Value - delta;
        }*/
        public IObservable<T> GetObsOnChange()
        {
            return onValueChanged;
        }
        #endregion

        public static implicit operator T(DeltaBase<T> deltaValue)
        {
            return deltaValue.Value;
        }

        /// <summary>
        /// GetModifiedValue
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return baseModField.Value;
        }

        /// <summary>
        /// Set BaseValue
        /// </summary>
        /// <param name="value"></param>
        public abstract void SetValue(T value);
        [FoldoutGroup(EditorConstants.debugGroup)]
        [ShowInInspector]
        public virtual void RemoveAllModsFromAllModifiables()
        {
            RemoveAllMods();
            GetIncreaseModProcessor().RemoveAllMods();
            GetDecreaseModProcessor().RemoveAllMods();
        }
    }
}