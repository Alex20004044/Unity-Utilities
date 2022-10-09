using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Sirenix.Serialization;

namespace MSFD
{
    public interface IDelta<T> : IModField<T>
    {
        T Increase(T value);
        T Decrease(T value);

        #region IDeltaModifiable
        IModProcessor<T> GetIncreaseModProcessor();
        IModProcessor<T> GetDecreaseModProcessor();
        /// <summary>
        /// Add mod both on Increase and Decrease functions
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        IDisposable AddChangeMod(Func<T, T> mod, int priority = 0);
        /// <summary>
        /// Remove mods from core modField, Increase and Decrease mods
        /// </summary>
        void RemoveAllModsFromAllModifiables();
        #endregion
        /// <summary>
        /// Return delta value from last change
        /// </summary>
        /// <returns></returns>
        IObservable<T> GetObsOnChange();
    }

    public static class IDeltaExtension
    {        
        /// <summary>
        /// Return Abs(delta)
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static IObservable<float> GetObsOnIncrease(this IDelta<float> delta)
        {
            return delta.GetObsOnChange().Where(x => x > 0);
        }
        /// <summary>
        /// Return Abs(delta)
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static IObservable<float> GetObsOnDecrease(this IDelta<float> delta)
        {
            return delta.GetObsOnChange().Where(x => x < 0).Select(x => -x);
        }
        /// <summary>
        /// Return Abs(delta)
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static IObservable<int> GetObsOnIncrease(this IDelta<int> delta)
        {
            return delta.GetObsOnChange().Where(x => x > 0);
        }
        /// <summary>
        /// Return Abs(delta)
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public static IObservable<int> GetObsOnDecrease(this IDelta<int> delta)
        {
            return delta.GetObsOnChange().Where(x => x < 0).Select(x => -x);
        }
    }
}
