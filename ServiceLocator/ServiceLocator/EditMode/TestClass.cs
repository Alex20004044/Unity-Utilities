using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : IObservable<float>, ITestInterface, ITestInterface2
{
    public IDisposable Subscribe(IObserver<float> observer)
    {
        throw new NotImplementedException();
    }
}

public interface ITestInterface
{

}
public interface ITestInterface2
{

}
public class NotInheritedClass
{

}