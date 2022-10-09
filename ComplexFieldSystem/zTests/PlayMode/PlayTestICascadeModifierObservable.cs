using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
using System;
using UniRx;

public class PlayTestICascadeModifier
{

    [UnityTest]
    public IEnumerator TestComplexFieldsICascadeModifier()
    {
        yield return PlayTestICascadeModifierFloat(new ModProcessor<float>());
        yield return PlayTestICascadeModifierFloat(new ModField<float>());
        yield return PlayTestICascadeModifierFloat(new Delta());
        yield return PlayTestICascadeModifierFloat(new DeltaE());
        yield return PlayTestICascadeModifierFloat(new DeltaRange());
        yield return PlayTestICascadeModifierFloat(new DeltaRangeE());
        yield return PlayTestICascadeModifierFloat(new Rechargable());
        yield return PlayTestICascadeModifierFloat(new RechargableE());
    }

    
    public static IEnumerator PlayTestICascadeModifierFloat(IModProcessor<float> cascadeModifier)
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
