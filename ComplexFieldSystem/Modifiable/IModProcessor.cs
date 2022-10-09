using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IModProcessor<T> : IModifiable<T>
    {
        T CalculateWithMods(T sourceValue);
    }
}