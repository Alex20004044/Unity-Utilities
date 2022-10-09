using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IFieldGetter<T>
    {
        T GetValue();
    }
}