using MSFD;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
public class ModifiableTest1 : MonoBehaviour
{
    [SerializeField]
    ModField<float> modifiableFieldFloat = new ModField<float>();

    CompositeDisposable disposables = new CompositeDisposable();
    IDisposable disp1;
    private void Start()
    {
        modifiableFieldFloat.GetObsOnModsUpdated().Subscribe((x) => Debug.Log("Modifiers are changed"));
        modifiableFieldFloat.Subscribe((x) => Debug.Log("Value is changed: " + x));
    }



    [Button]
    void Add1()
    {
        disp1 = modifiableFieldFloat.AddMod((x) => x * 2);
    }
    [Button]
    void Remove()
    {
        disp1.Dispose();
    }
}
