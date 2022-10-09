using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace MSFD
{
    public interface IClip<T>: IRechargable<T>
    {
        IRechargable<T> GetAmmo(); // [0:100%]
        IDelta<T> GetShootCost();
        IRechargable<T> GetReloading(); // [0:+infinity]
        IDelta<T> GetReloadingTime();
        #region Shoot
        //Empty => ammo = 0
        //Fill => ammo = 100%, delayBetweenShoot = 0
        //Increase => ammo.Increase()
        //Decrease => ammo.Decrease()

        bool TrySpecialShoot(T shootCost, float reloadingTime);
        bool IsCanSpecialShoot(T shootCost);
        bool IsCanShoot();
        bool TryShoot();
        IObservable<Unit> GetObsOnCanShoot();
        IObservable<Unit> GetObsOnShoot();

        #endregion
    }
}