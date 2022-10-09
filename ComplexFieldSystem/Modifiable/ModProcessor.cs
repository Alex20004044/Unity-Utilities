using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
#if UNITY_EDITOR
using System.Reflection;
#endif
namespace MSFD
{
    [Serializable]
    public class ModProcessor<T> : IModProcessor<T>
    {
        SortedDictionary<int, List<Func<T, T>>> mods = new SortedDictionary<int, List<Func<T, T>>>();
        CompositeDisposable disposables = new CompositeDisposable();
        Subject<Unit> onModsUpdated = new Subject<Unit>();

        /// <summary>
        /// Higher priority functions are called first
        /// </summary>
        /// <param name="func"></param>
        /// <param name="priority"></param>
        public IDisposable AddMod(Func<T, T> mod, int priority = 0)
        {
            priority = -priority;
            List<Func<T, T>> mods;
            if (!this.mods.TryGetValue(priority, out mods))
            {
                mods = new List<Func<T, T>>();
                this.mods.Add(priority, mods);
            }
            mods.Add(mod);
            onModsUpdated.OnNext(Unit.Default);

            IDisposable disposable = Disposable.Create(() => { 
                mods.Remove(mod); 
                onModsUpdated.OnNext(Unit.Default); });
            disposables.Add(disposable);
            return disposable;
        }
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public T CalculateWithMods(T sourceValue)
        {
            foreach (var x in mods)
            {
                List<Func<T, T>> mods = x.Value;
                for (int i = 0; i < mods.Count; i++)
                {
                    sourceValue = mods[i].Invoke(sourceValue);
                }
            }
            return sourceValue;
        }

        public IObservable<Unit> GetObsOnModsUpdated()
        {
            return onModsUpdated.ThrottleFrame(0, FrameCountType.EndOfFrame);
        }

        public void RaiseModsUpdatedEvent()
        {
            onModsUpdated.OnNext(Unit.Default);
        }

        [FoldoutGroup(EditorConstants.debugGroup)]
        [Button]
        public void RemoveAllMods()
        {
            disposables.Clear();
            onModsUpdated.OnNext(Unit.Default);
        }


#if UNITY_EDITOR
        [FoldoutGroup(EditorConstants.debugGroup)]
        [Obsolete(EditorConstants.editorOnly)]
        [Button]
        void ShowInstalledModifiers()
        {
            string log = "CascadeModifier<" + typeof(T) + ">\n";
            foreach (var x in mods)
            {
                log += "Priority: " + x.Key;
                foreach (var func in x.Value)
                {
                    log += " Modifier name: " + func.GetMethodInfo().Name;
                }
                log += "\n";
            }
            Debug.Log(log);
        }

#endif
    }
}