using System.Collections;
using System.Collections.Generic;
using MSFD;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
public class PlayTestIDeltaObservable
{
    /// <summary>
    /// This Tests can be moved to EditMode
    /// </summary>
    /// <returns></returns>

    [UnityTest]
    public IEnumerator Delta()
    {
        var x = new Delta();
        return PlayTestIDeltaFloat(x);
    }

    [UnityTest]
    public IEnumerator DeltaRange()
    {
        var x = new DeltaRange();
        x.GetMaxBorderModField().SetValue(1000); 
        x.GetMinBorderModField().SetValue(-1000);

        return PlayTestIDeltaFloat(x);
    }

    [UnityTest]
    public IEnumerator Rechargable()
    {
        var x = new Rechargable();
        x.GetMaxBorderModField().SetValue(1000);
        x.GetMinBorderModField().SetValue(-1000);
        return PlayTestIDeltaFloat(x);
    }

    public static IEnumerator PlayTestIDeltaFloat(IDelta<float> delta)
    {
        delta.SetValue(10);
        bool checkOnIncrease = false;
        bool checkOnDecrease = false;
        float d = 0;
        var disposableOnIncrease = delta.GetObsOnIncrease().Subscribe((x) => { checkOnIncrease = true; d = x; });
        delta.Increase(2);
        Assert.AreEqual(2, d);
        Assert.IsTrue(checkOnIncrease);
        Assert.IsFalse(checkOnDecrease);

        checkOnIncrease = false;
        checkOnDecrease = false;
        d = 0;

        var disposableOnDecrease = delta.GetObsOnDecrease().Subscribe((x) => { checkOnDecrease = true; d = x; });
        delta.Decrease(2);
        Assert.AreEqual(2, d);
        Assert.IsFalse(checkOnIncrease);
        Assert.IsTrue(checkOnDecrease);

        checkOnIncrease = false;
        checkOnDecrease = false;
        d = 0;
        delta.GetIncreaseModProcessor().AddMod((x) => x * 2);
        delta.Increase(2);
        Assert.AreEqual(4, d);
        Assert.IsTrue(checkOnIncrease);
        Assert.IsFalse(checkOnDecrease);

        checkOnIncrease = false;
        checkOnDecrease = false;
        d = 0;
        delta.GetDecreaseModProcessor().AddMod((x) => x * 2);
        delta.Decrease(2);
        Assert.AreEqual(4, d);
        Assert.IsFalse(checkOnIncrease);
        Assert.IsTrue(checkOnDecrease);

        disposableOnIncrease.Dispose();
        disposableOnDecrease.Dispose();

        delta.AddChangeMod((x) => x * 3);
        var disposableOnChange = delta.GetObsOnChange().Subscribe((x) => { d = x; checkOnIncrease = true; checkOnDecrease = true; });
        
        checkOnIncrease = false;
        checkOnDecrease = false;
        d = 0;
        delta.SetValue(10);
        delta.Increase(2);
        Assert.AreEqual(12, d);
        Assert.AreEqual(22, delta.Value);
        Assert.IsTrue(checkOnIncrease);
        Assert.IsTrue(checkOnDecrease);        
        checkOnIncrease = false;
        checkOnDecrease = false;
        
        d = 0;
        delta.SetValue(10);
        delta.Decrease(2);
        Assert.AreEqual(-12, d);
        Assert.AreEqual(-2, delta.Value);
        Assert.IsTrue(checkOnIncrease);
        Assert.IsTrue(checkOnDecrease);


        delta.RemoveAllModsFromAllModifiables();
        delta.SetValue(10);
        delta.Decrease(2);
        Assert.AreEqual(8, delta.Value);
        Assert.AreEqual(-2, d);
        delta.Increase(2);
        Assert.AreEqual(10, delta.Value);
        Assert.AreEqual(2, d);


        yield return null;
    }
}
