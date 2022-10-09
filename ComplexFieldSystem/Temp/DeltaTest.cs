using MSFD;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTest : MonoBehaviour
{
    [SerializeField]
    Delta money = new Delta();    
    [SerializeField]
    DeltaE moneyE = new DeltaE();

    IDisposable disposable;IDisposable disposable2;

    [Button]
    void UpgradeMoney()
    {
        disposable = money.GetIncreaseModProcessor().AddMod((x) => x * 2);
    }
    [Button]
    void RemoveUpgrade()
    {
        disposable.Dispose();
    }
    [Button]
    void X3Money()
    {
        disposable2 = money.AddMod((x) => x * 3);
    }
    [Button]
    void RemoveX3Money()
    {
        disposable2.Dispose();
    }
}
