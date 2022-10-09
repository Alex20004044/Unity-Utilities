using System;
using UnityEngine;

namespace MSFD
{
    public interface IDeltaRange<T> : IDelta<T>, IObservable<IDeltaRange<T>>
    {
        /// <summary>
        /// Set Value to MinBorder
        /// </summary>
        void Empty();
        /// <summary>
        /// Set Value to MaxBorder
        /// </summary>
        void Fill();

        bool IsEmpty();
        bool IsFull();

        T MinBorder { get; }

        T MaxBorder { get; }

        #region IDeltaRangeModifiable
        IModField<T> GetMinBorderModField();
        IModField<T> GetMaxBorderModField();
        #endregion

        /// <summary>
        /// Return abs(delta)
        /// </summary>
        /// <returns></returns>
        IObservable<T> GetObsOnMinBorder();
        /// <summary>
        /// Return abs(delta)
        /// </summary>
        /// <returns></returns>
        IObservable<T> GetObsOnMaxBorder();
        /// <summary>
        /// Called when GetObsOnMin() or GetObsOnMax()
        /// Return delta
        /// </summary>
        /// <returns></returns>
        IObservable<T> GetObsOnRangeReached();
    }
    public static class IDeltaRangeExtension
    {
        public static float GetFillPercent(this IDeltaRange<float> deltaRange)
        {
            return AS.Calculation.Map(deltaRange.Value, deltaRange.MinBorder, deltaRange.MaxBorder, 0f, 1f);
        }        
        public static float GetFillPercent(this IDeltaRange<int> deltaRange)
        {
            return AS.Calculation.Map(deltaRange.Value, deltaRange.MinBorder, deltaRange.MaxBorder, 0f, 1f);
        }
    }
}
