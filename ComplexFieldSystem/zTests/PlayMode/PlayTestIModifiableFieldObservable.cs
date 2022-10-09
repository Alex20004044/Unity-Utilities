using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MSFD;
using UniRx;
public class PlayTestIModifiableFieldObservable
{

    [UnityTest]
    public IEnumerator ModifiableField()
    {
        var x = new ModField<float>();
        return PlayTestIModifiableFieldFloat(x);
    }
    [UnityTest]
    public IEnumerator Delta()
    {
        var x = new Delta();
        return PlayTestIModifiableFieldFloat(x);
    }

    [UnityTest]
    public IEnumerator DeltaRange()
    {
        var x = new DeltaRange();
        x.GetMaxBorderModField().SetValue(1000);
        return PlayTestIModifiableFieldFloat(x);
    }     
    [UnityTest]
    public IEnumerator DeltaRangeMinMaxModifiable()
    {
        var x = new DeltaRange();
        x.GetMaxBorderModField().SetValue(1000);
        yield return PlayTestIModifiableFieldFloat(x.GetMaxBorderModField());
        yield return PlayTestIModifiableFieldFloat(x.GetMinBorderModField());
    }    
    [UnityTest]
    public IEnumerator Rechargable()
    {
        var x = new Rechargable();
        x.GetMaxBorderModField().SetValue(1000);
        return PlayTestIModifiableFieldFloat(x);
    }


    public static IEnumerator PlayTestIModifiableFieldFloat(IModField<float> modifiableField)
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

        float observeValue = 0;

        var disposable = modifiableField.Subscribe((x) => observeValue = x);
        Assert.AreEqual(999, observeValue);

        modifiableField.AddMod((x) => x / 3);
        yield return null;
        Assert.AreEqual(333, observeValue);

        modifiableField.RemoveAllMods();
        yield return null;
        Assert.AreEqual(999, observeValue);
        disposable.Dispose();
    }
}
