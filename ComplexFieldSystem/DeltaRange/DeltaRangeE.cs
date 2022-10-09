using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace MSFD
{
    [System.Serializable]
    public class DeltaRangeE : DeltaRange
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
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onMinBorder = new UnityEvent();
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onMaxBorder = new UnityEvent();
        [FoldoutGroup(EditorConstants.eventsGroup)]
        [SerializeField]
        UnityEvent onRangeReached = new UnityEvent();

        public DeltaRangeE() : base()
        {
            Initialize();
        }
        public DeltaRangeE(float value, float minBorder, float maxBorder) : base(value, minBorder, maxBorder)
        {
            Initialize();
        }
        void Initialize()
        {
            this.GetObsOnIncrease().Subscribe((x) => onIncrease.Invoke());
            this.GetObsOnDecrease().Subscribe((x) => onDecrease.Invoke());
            GetObsOnChange().Subscribe((x) => onChange.Invoke());

            GetObsOnMinBorder().Subscribe((x) => onMinBorder.Invoke());
            GetObsOnMaxBorder().Subscribe((x) => onMaxBorder.Invoke());
            GetObsOnRangeReached().Subscribe((x) => onRangeReached.Invoke());
        }
    }
}