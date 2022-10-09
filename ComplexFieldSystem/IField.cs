using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public interface IField<T>: IFieldGetter<T>, IFieldSetter<T>
    {
    }
}