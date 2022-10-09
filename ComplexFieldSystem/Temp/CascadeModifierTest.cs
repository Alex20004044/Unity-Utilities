using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Sirenix.OdinInspector;
using System;

namespace MSFD.Test
{
    public class CascadeModifierTest : MonoBehaviour
    {
        [SerializeField]
        ModProcessor<float> cascadeModifierCore = new ModProcessor<float>();

        CompositeDisposable disposables = new CompositeDisposable();
        IDisposable disp1;
        private void Start()
        {
            cascadeModifierCore.GetObsOnModsUpdated().Subscribe((x) => Debug.Log("Modifiers are changed"));
        }

       

        [Button]
        void Add1()
        {
            disp1 = cascadeModifierCore.AddMod((x) => x * 2);
        }
        [Button]
        void Remove()
        {
            disp1.Dispose();
        }
    }
}