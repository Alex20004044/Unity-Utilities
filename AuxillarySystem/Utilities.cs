using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.AS
{
    public static class Utilities
    {
        #region Debug
        public static void LogError(string message, bool isLog = true)
        {
            if (isLog)
            {
                Debug.LogError(message);
            }
        }
        public static void LogWarning(string message, bool isLog = true)
        {
            if (isLog)
            {
                Debug.LogWarning(message);
            }
        }
        public static void Log(string message, bool isLog = true)
        {
            if(isLog)
            {
                Debug.Log(message);
            }
        }
        #endregion
        #region Components
        /// <summary>
        /// Attempt to use GetComponent(), if not found => attempt to use ComponentInChildren()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T FindComponent<T>(this MonoBehaviour source)
        {
            T target;
            target = source.GetComponent<T>();
            if (target == null)
            {
                target = source.GetComponentInChildren<T>();
                if (target == null)
                {
                    Debug.LogError("Can't find component " + typeof(T).Name + " in " + source.name);
                }
            }
            return target;
        }
        /// <summary>
        /// Attempt to use GetComponent(), if not found => attempt to use ComponentInChildren()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Component FindComponent(this MonoBehaviour source, Type type)
        {
            Component target;
            target = source.GetComponent(type);
            if (target == null)
            {
                target = source.GetComponentInChildren(type);
                if (target == null)
                {
                    Debug.LogError("Can't find component " + type.Name + " in " + source.name);
                }
            }
            return target;
        }

        /// <summary>
        /// Attempt to use GetComponent(), if not found => attempt to use ComponentInChildren()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T FindComponent<T>(GameObject source)
        {
            T target;
            target = source.GetComponent<T>();
            if (target == null)
            {
                target = source.GetComponentInChildren<T>();
                if (target == null)
                {
                    Debug.LogError("Can't find component " + typeof(T).Name + " in " + source.name);
                }
            }
            return target;
        }
        public static Component FindComponent(GameObject source, Type type)
        {
            Component target;
            target = source.GetComponent(type);
            if (target == null)
            {
                target = source.GetComponentInChildren(type);
                if (target == null)
                {
                    Debug.LogError("Can't find component " + type.Name + " in " + source.name);
                }
            }
            return target;
        }

        #endregion
        #region Collections
        public static bool CompareLabels(string[] label, string[] targetLabels)
        {
            bool result = false;
            for (int i = 0; i < label.Length && !result; i++)
            {
                result = CompareLabel(label[i], targetLabels);
            }
            return result;
        }
        public static bool CompareLabel(string label, string[] targetLabels)
        {
            for (int i = 0; i < targetLabels.Length; i++)
            {
                if (label.Equals(targetLabels[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CompareLayers(int layer, LayerMask targetLayers)
        {
            return (targetLayers & 1 << layer) != 0;
        }

        public static T FindDesiredItem<T>(List<T> dataList, System.Func<T, T, bool> compareIsFirstItemBetter)
        {
            T best = dataList[0];

            for (int i = 1; i < dataList.Count; i++)
            {
                if (compareIsFirstItemBetter(dataList[i], best))
                {
                    best = dataList[i];
                }
            }
            return best;
        }

        public static bool RemoveDuplicateReferences<T>(List<T> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list.LastIndexOf(list[i]) != i)
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        public static bool RemoveNullReferences<T>(List<T> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        /// <summary>
        /// Additionally cheks "null" name
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool RemoveNullReferencesSO(List<ScriptableObject> list)
        {
            bool isCollectionModified = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null || list[i].ToString() == "null")
                {
                    list.RemoveAt(i);
                    isCollectionModified = true;
                    i--;
                }
            }
            return isCollectionModified;
        }
        #endregion
        #region Converter
        public static List<V> ListConverter<T, V>(List<T> ts, System.Func<T, V> convertFunc)
        {
            List<V> vs = new List<V>(ts.Count);

            for (int i = 0; i < ts.Count; i++)
            {
                vs.Add(convertFunc(ts[i]));
            }
            return vs;
        }
        public static V[] ArrayConverter<T, V>(T[] ts, System.Func<T, V> convertFunc)
        {
            V[] vs = new V[ts.Length];
            for (int i = 0; i < ts.Length; i++)
            {
                vs[i] = convertFunc(ts[i]);
            }
            return vs;
        }
        #endregion
    }
}
