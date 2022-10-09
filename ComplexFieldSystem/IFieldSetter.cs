using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IFieldSetter<T>
    {
        void SetValue(T value);
    }
}