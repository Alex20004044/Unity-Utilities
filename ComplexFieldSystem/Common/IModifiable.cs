using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public interface IModifiable<T>
    {
        /// <summary>
        /// Mods with higher priority will be called first
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        IDisposable AddMod(Func<T, T> mod, int priority = 0);
        /// <summary>
        /// This event raised on the next frame after changes happened, to prevent DDos when several modifiers were installed
        /// </summary>
        /// <returns></returns>
        IObservable<Unit> GetObsOnModsUpdated();
        void RemoveAllMods();
        void RaiseModsUpdatedEvent();
    }

    public static class ModifiableExtension
    {
        /// <summary>
        /// Is it neccessary? Func can hold several mods as delegate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modifiable"></param>
        /// <param name="modifiers"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
/*        public static IDisposable AddModifiers<T>(this IModifiable<T> modifiable, Func<T, T>[] modifiers, int priority = 0)
        {
            CompositeDisposable disposables = new CompositeDisposable();

            foreach (var x in modifiers)
                disposables.Add(modifiable.AddModifier(x, priority));

            return disposables;
        }*/
    }
}