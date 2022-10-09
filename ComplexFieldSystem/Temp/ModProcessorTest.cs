using MSFD;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModProcessorTest : MonoBehaviour
{
    [SerializeField]
    ModProcessor<float> modProcessor = new ModProcessor<float>();

    IDisposable disposableX2;
    IDisposable disposablePlus2;
    [Button]
    public void AddX2()
    {
        disposableX2 = modProcessor.AddMod((x) => x * 2);
    }
    [Button]
    public void RemoveX2()
    {
        disposableX2.Dispose();
    }

    [Button]
    public void AddPlus2()
    {
        disposablePlus2 = modProcessor.AddMod((x) => x + 2);
    }
    [Button]
    public void RemovePlus2()
    {
        disposablePlus2.Dispose();
    }
}
