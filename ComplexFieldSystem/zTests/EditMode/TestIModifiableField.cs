using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
using System;

public class TestIModifiableField
{
    [Test]
    public void ModifiableField()
    {
        TestIModifiableFieldFloat(new ModField<float>());
    }
    [Test]
    public void Delta()
    {
        TestIModifiableFieldFloat(new Delta());
    }
    [Test]
    public void DeltaE()
    {
        TestIModifiableFieldFloat(new DeltaE());
    }
    [Test]
    public void DeltaRange()
    {
        var x = new DeltaRange();
        x.GetMaxBorderModField().SetValue(1000);
        TestIModifiableFieldFloat(x);
    }
    [Test]
    public void DeltaRangeE()
    {
        var x = new DeltaRangeE();
        x.GetMaxBorderModField().SetValue(1000);
        TestIModifiableFieldFloat(x);
    }
    [Test]
    public void Rechargable()
    {
        var x = new Rechargable();
        x.GetMaxBorderModField().SetValue(1000);
        TestIModifiableFieldFloat(x);
    }
    [Test]
    public void RechargableE()
    {
        var x = new RechargableE();
        x.GetMaxBorderModField().SetValue(1000);
        TestIModifiableFieldFloat(x);
    }

    public static void TestIModifiableFieldFloat(IModField<float> modifiableField)
    {
        Assert.AreEqual(modifiableField.Value, modifiableField.BaseValue);

        modifiableField.AddMod((x) => x * 3);

        modifiableField.BaseValue = 2;

        Assert.AreEqual(modifiableField.Value, modifiableField.BaseValue * 3);

        modifiableField.BaseValue = -10;
        Assert.AreEqual(modifiableField.Value, modifiableField.BaseValue * 3);

        modifiableField.RemoveAllMods();

        modifiableField.SetValue(999);
        Assert.AreEqual(modifiableField.Value, modifiableField.GetValue());
        Assert.AreEqual(modifiableField.Value, 999);
    }
}
