using System.Collections;
using System.Collections.Generic;
using MSFD;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestIRechargable
{
    [UnityTest]
    public IEnumerator Rechargable()
    {
        var x = new Rechargable();
        return TestIRechragableFloat(x);
    }
    [UnityTest]
    public IEnumerator Clip()
    {
        var x = new Clip();
        return TestIRechragableFloat(x);
    }

    public static IEnumerator TestIRechragableFloat(IRechargable<float> rechargable)
    {
        rechargable.SetTimeMode(IRechargable<float>.TimeMode.realTime);
        //Assert.AreEqual(IRechargable<float>.TimeMode.realTime, rechargable.GetTimeMode());
        yield return null;
    }
}
