using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;

namespace MSFD
{
    [System.Serializable]
    public abstract class DeltaRangeBase<T> : DeltaBase<T>, IDeltaRange<T>
    {

        [InlineProperty]
        [SerializeField]
        protected ModField<T> minBorder = new ModField<T>();
        [InlineProperty]
        [SerializeField]
        protected ModField<T> maxBorder = new ModField<T>();

        protected Subject<T> onMinBorderSubject = new Subject<T>();
        protected Subject<T> onMaxBorderSubject = new Subject<T>();

#if UNITY_EDITOR
        [HideLabel]
        [OnStateUpdate("@__progressBar = BaseValue")]
        [ProgressBar("$minBorder", "$maxBorder")]
        [ShowInInspector]
        [Obsolete(EditorConstants.editorOnly)]
        T __progressBar;
#endif
        public DeltaRangeBase(T value, T minBorder, T maxBorder)
        {
            this.minBorder.SetValue(minBorder);
            this.maxBorder.SetValue(maxBorder);
            SetValue(value);

            this.minBorder.Subscribe((x) => SetValue(Value));
            this.maxBorder.Subscribe((x) => SetValue(Value));
        }
        //Attention! What behaviour threre should be?
        /*public override IDisposable AddMod(Func<T, T> modifier, int priority = 0)
        {
            CompositeDisposable disposables = new CompositeDisposable();
            disposables.Add(base.AddMod(modifier, priority));
            disposables.Add(minBorder.AddMod(modifier, priority));
            disposables.Add(maxBorder.AddMod(modifier, priority));
            return disposables;
        }*/

        public IModField<T> GetMinBorderModField()
        {
            return minBorder;
        }
        public IModField<T> GetMaxBorderModField()
        {
            return maxBorder;
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public void Empty()
        {
            SetValue(minBorder.GetValue());
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public void Fill()
        {
            SetValue(maxBorder.GetValue());
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public abstract bool IsEmpty();
        /*{
            return Value <= minBorder.Value;
        }*/
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public abstract bool IsFull();
        /*{
            return Value >= maxBorder.Value;
        }*/
        public IObservable<T> GetObsOnMinBorder()
        {
            return onMinBorderSubject;
        }
        public IObservable<T> GetObsOnMaxBorder()
        {
            return onMaxBorderSubject;
        }
        public abstract IObservable<T> GetObsOnRangeReached();
        /*{
            return Observable.Merge(GetObsOnMax(), GetObsOnMin().Select((x) => -x));
        }*/
        public IDisposable Subscribe(IObserver<IDeltaRange<T>> observer)
        {
            observer.OnNext(this);
            return Observable.Merge(baseModField, minBorder, maxBorder).ThrottleFirstFrame(0, FrameCountType.EndOfFrame).Subscribe(((x) => observer.OnNext(this)));
        }

        public T MaxBorder => maxBorder.Value;

        public T MinBorder => minBorder.Value;
        public override void RemoveAllModsFromAllModifiables()
        {
            base.RemoveAllModsFromAllModifiables();
            GetMinBorderModField().RemoveAllMods();
            GetMaxBorderModField().RemoveAllMods();
        }

        public static implicit operator T(DeltaRangeBase<T> deltaValue)
        {
            return deltaValue.Value;
        }
    }
}