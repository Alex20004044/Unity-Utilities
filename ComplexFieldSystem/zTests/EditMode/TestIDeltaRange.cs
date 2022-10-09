using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
using UniRx;

public class TestIDeltaRange
{
    [UnityTest]
    public IEnumerator DeltaRange()
    {
        var x = new DeltaRange();
        return TestIDeltaRangeFloat(x);
    }
    [UnityTest]
    public IEnumerator Rechargable()
    {
        var x = new Rechargable();
        return TestIDeltaRangeFloat(x);
    }

    public static IEnumerator TestIDeltaRangeFloat(IDeltaRange<float> deltaRange)
    {
        deltaRange.GetMinBorderModField().SetValue(0);
        deltaRange.GetMaxBorderModField().SetValue(200);
        deltaRange.SetValue(100);

        bool isMinBorderReached = false;
        bool isMaxBorderReached = false;
        CompositeDisposable disposables = new CompositeDisposable();
        disposables.Add(deltaRange.GetObsOnMinBorder().Subscribe(x => isMinBorderReached = true));
        disposables.Add(deltaRange.GetObsOnMaxBorder().Subscribe(x => isMaxBorderReached = true));

        Assert.That(deltaRange.MinBorder == 0);
        Assert.That(deltaRange.MaxBorder == 200);

        Assert.That(!deltaRange.IsEmpty());
        Assert.That(!deltaRange.IsFull());

        Assert.That(!isMinBorderReached && !isMinBorderReached);

        deltaRange.SetValue(deltaRange.MinBorder - 100);
        Assert.That(deltaRange.Value == deltaRange.MinBorder);

        Assert.That(deltaRange.IsEmpty());
        Assert.That(!deltaRange.IsFull());        
        
        Assert.That(isMinBorderReached && !isMaxBorderReached);

        isMinBorderReached = false;
        isMaxBorderReached = false;
        deltaRange.SetValue(deltaRange.MaxBorder + 100);
        Assert.That(deltaRange.Value == deltaRange.MaxBorder);

        Assert.That(!deltaRange.IsEmpty());
        Assert.That(deltaRange.IsFull());

        Assert.That(!isMinBorderReached && isMaxBorderReached);

        isMinBorderReached = false;
        isMaxBorderReached = false;
        disposables.Clear();

        bool isBorderReached = false;
        deltaRange.GetObsOnRangeReached().Subscribe((x) => isBorderReached = true);
        deltaRange.SetValue(deltaRange.MaxBorder / 2);

        Assert.That(!deltaRange.IsEmpty() && !deltaRange.IsFull() && !isBorderReached);

        deltaRange.Empty();
        Assert.That(deltaRange.IsEmpty() && !deltaRange.IsFull() && isBorderReached);

        isBorderReached = false;
        deltaRange.Fill();
        Assert.That(!deltaRange.IsEmpty() && deltaRange.IsFull() && isBorderReached);

        //Mods

        deltaRange.GetMaxBorderModField().AddMod((x) => x * 2);
        Assert.That(deltaRange.MaxBorder == 400);
        deltaRange.GetMaxBorderModField().SetValue(300);
        Assert.That(deltaRange.MaxBorder == 600);
        
        deltaRange.SetValue(50);
        deltaRange.GetMinBorderModField().AddMod((x) => x + 100);
        //Mod update event comes in a next frame
        yield return null;
        Assert.AreEqual(100, deltaRange.MinBorder);
        Assert.AreEqual(100, deltaRange.Value);
        Assert.That(deltaRange.IsEmpty());
        
        deltaRange.GetMinBorderModField().SetValue(50);
        yield return null;
        Assert.That(deltaRange.MinBorder == 150);
        Assert.That(deltaRange.Value == 150);
        Assert.That(deltaRange.IsEmpty());

        deltaRange.SetValue(500);
        deltaRange.GetMaxBorderModField().SetValue(200);
        yield return null;

        Assert.That(deltaRange.MaxBorder == 400);
        Assert.That(deltaRange.Value == 400);
        Assert.That(deltaRange.IsFull());

        deltaRange.SetValue(700);
        Assert.That(deltaRange.Value == 400);
        Assert.That(deltaRange.IsFull());

        deltaRange.SetValue(-100);
        Assert.That(deltaRange.Value == 150);
        Assert.That(deltaRange.IsEmpty());

        Assert.AreEqual(MSFD.AS.Calculation.Map(deltaRange.Value, deltaRange.MinBorder, deltaRange.MaxBorder, 0f, 1f), deltaRange.GetFillPercent());
    }
}
