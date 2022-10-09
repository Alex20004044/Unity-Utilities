using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
using System;

public class TestICascadeModifier
{
    // A Test behaves as an ordinary method
    [Test]
    public void CascadeModifierCore()
    {
        TestICascadeModifierFloat(new ModProcessor<float>());
    }
    [Test]
    public void ModifiableField()
    {
        TestICascadeModifierFloat(new ModField<float>());
    }
    [Test]
    public void Delta()
    {
        TestICascadeModifierFloat(new Delta());
    }
    [Test]
    public void DeltaE()
    {
        TestICascadeModifierFloat(new DeltaE());
    }
    [Test]
    public void DeltaRange()
    {
        TestICascadeModifierFloat(new DeltaRange());
    }
    [Test]
    public void DeltaRangeE()
    {
        TestICascadeModifierFloat(new DeltaRangeE());
    }
    [Test]
    public void Rechargable()
    {
        TestICascadeModifierFloat(new Rechargable());
    }
    [Test]
    public void RechargableE()
    {
        TestICascadeModifierFloat(new RechargableE());
    }

    public static void TestICascadeModifierFloat(IModProcessor<float> cascadeModifier)
    {
        float value;

        value = 12;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));

        IDisposable x2Disposable = cascadeModifier.AddMod((x) => x * 2);

        value = 12;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));

        cascadeModifier.AddMod((x) => x + 10);
        value = 12;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));

        cascadeModifier.AddMod((x) => x - 5, 10);
        value = 12;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));

        x2Disposable.Dispose();
        value = 12;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));

        cascadeModifier.RemoveAllMods();

        value = 12;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
    }

    public static IEnumerator TestICascadeModifierFloatOnModifiersChanged(IModProcessor<float> cascadeModifier)
    {
        float value;
        bool isModifersChangeDetected = false;

        IObservable<UniRx.Unit> onModifiersChanged = cascadeModifier.GetObsOnModsUpdated();
        IDisposable onModifiersChangedDisposable = onModifiersChanged.Subscribe((x) => isModifersChangeDetected = true);

        value = 12;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));

        yield return null;
        Assert.IsFalse(isModifersChangeDetected);

        IDisposable x2Disposable = cascadeModifier.AddMod((x) => x * 2);

        yield return null;
        Assert.IsTrue(isModifersChangeDetected);

        value = 12;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value * 2, cascadeModifier.CalculateWithMods(value));

        cascadeModifier.AddMod((x) => x + 10);
        value = 12;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value * 2 + 10, cascadeModifier.CalculateWithMods(value));

        cascadeModifier.AddMod((x) => x - 5, 10);
        value = 12;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual((value - 5) * 2 + 10, cascadeModifier.CalculateWithMods(value));

        x2Disposable.Dispose();
        value = 12;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual((value - 5) + 10, cascadeModifier.CalculateWithMods(value));

        isModifersChangeDetected = false;
        cascadeModifier.RemoveAllMods();
        yield return null;
        Assert.IsTrue(isModifersChangeDetected);

        value = 12;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = 0;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));
        value = -11;
        Assert.AreEqual(value, cascadeModifier.CalculateWithMods(value));

        onModifiersChangedDisposable.Dispose();
        isModifersChangeDetected = false;
        cascadeModifier.AddMod((x) => x * 3).Dispose();
        yield return null;
        Assert.IsFalse(isModifersChangeDetected);

        cascadeModifier.RemoveAllMods();
    }

}
