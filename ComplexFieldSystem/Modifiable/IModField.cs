using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IModField<T> : IField<T>, IModProcessor<T>, IObservable<T>
    {
        /// <summary>
        /// Modified Value
        /// </summary>
        T Value { get; }
        /// <summary>
        /// Value without mods
        /// </summary>
        T BaseValue { get; set; }
    }
}