using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    [System.Serializable]
    public class DeltaIntE : DeltaInt
    {
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onIncrease = new UnityEvent();
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onDecrease = new UnityEvent();
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onChange = new UnityEvent();

        public DeltaIntE() : base()
        {
            Initialize();
        }
        public DeltaIntE(int value) : base(value)
        {
            Initialize();
        }
        void Initialize()
        {
            this.GetObsOnIncrease().Subscribe((x) => onIncrease.Invoke());
            this.GetObsOnDecrease().Subscribe((x) => onDecrease.Invoke());
            GetObsOnChange().Subscribe((x) => onChange.Invoke());
        }
    }
}