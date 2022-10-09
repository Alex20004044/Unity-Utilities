using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MSFD;
using UniRx;
using Sirenix.OdinInspector;

public class DeltaRangeBorderTest : MonoBehaviour
{
    [SerializeField]
    DeltaRange deltaRange = new DeltaRange();
    [Button]
    void Start()
    {
        deltaRange.SetValue(10);
        deltaRange.GetMinBorderModField().SetValue(50);
    }


}
