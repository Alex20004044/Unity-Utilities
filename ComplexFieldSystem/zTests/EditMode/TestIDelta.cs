using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
public class TestIDelta
{
    [Test]
    public void Delta()
    {
        var x = new Delta();
        TestIDeltaFloat(x);
    }
    [Test]
    public void DeltaRange()
    {
        var x = new DeltaRange();
        x.GetMinBorderModField().SetValue(-1000);
        x.GetMaxBorderModField().SetValue(1000);
        TestIDeltaFloat(x);
    }
    [Test]
    public void Rechargable()
    {
        var x = new Rechargable();
        x.GetMinBorderModField().SetValue(-1000);
        x.GetMaxBorderModField().SetValue(1000);
        TestIDeltaFloat(x);
    }

    public static void TestIDeltaFloat(IDelta<float> delta)
    {
        delta.SetValue(10);

        delta.Increase(10);
        Assert.AreEqual(20, delta.Value);

        delta.Decrease(40);
        Assert.AreEqual(-20, delta.Value);

        delta.GetIncreaseModProcessor().AddMod((x) => x + 1);
        delta.Increase(1);
        delta.Increase(2);
        Assert.AreEqual(-15, delta.Value);

        delta.GetDecreaseModProcessor().AddMod((x) => x - 1);
        delta.Decrease(1);
        delta.Decrease(2);
        Assert.AreEqual(-16, delta.Value);

        delta.RemoveAllModsFromAllModifiables();
        delta.SetValue(10);

        delta.Increase(10);
        Assert.AreEqual(20, delta.Value);

        delta.Decrease(40);
        Assert.AreEqual(-20, delta.Value);

        delta.AddChangeMod((x) => x / 2);
        delta.SetValue(10);
        delta.Increase(5);
        Assert.AreEqual(12.5f, delta.Value);
        delta.Decrease(10);
        Assert.AreEqual(7.5f, delta.Value);
    }
}
