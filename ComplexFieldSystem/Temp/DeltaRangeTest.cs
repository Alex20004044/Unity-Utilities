using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class DeltaRangeTest : MonoBehaviour
{
    [SerializeField]
    DeltaRange rangeFloat = new DeltaRange(10, 3, 130);

    void Start()
    {
        rangeFloat.Subscribe((IDeltaRange<float> x) => Debug.Log("Value: " + x.Value + 
            " MinBorder: " + x.MinBorder + " MaxBorder: " + x.MaxBorder));
    }

    [Button]
    void AddMod()
    {
        rangeFloat.AddMod((x) => x * 2);
    }
    [Button]
    void AddMinBorderMod()
    {
        rangeFloat.GetMinBorderModField().AddMod((x) => x - 2);
    }
    [Button]
    void AddMaxBorderMod()
    {
        rangeFloat.GetMaxBorderModField().AddMod((x) => x +2002);
    }
    [Button]
    void RemoveAllMods()
    {
        rangeFloat.RemoveAllModsFromAllModifiables();
    }
}
