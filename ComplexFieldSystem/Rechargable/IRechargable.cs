using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public interface IRechargable<T>: IDeltaRange<T>
    {
        IDelta<T> GetRechargeSpeed();
        void StopRecharge();
        void StartRecharge();
        IObservable<bool> GetObsIsRechargeStarted();
        bool IsRechargeStarted();
        void SetTimeMode(TimeMode timeMode = TimeMode.scaledTime);
        //TimeMode GetTimeMode();
        enum TimeMode { scaledTime, realTime };
    }
}