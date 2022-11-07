using MSFD;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
public class ServiceLocatorTest
{
    [TearDown]
    public void TearDown()
    {
        ServiceLocator.Clear();
    }
    #region Register
    [Test]
    public void RegisterWithNull()
    {
        Assert.Throws<NullReferenceException>(() => ServiceLocator.Register<IObservable<float>>(null));
        Assert.Throws<NullReferenceException>(() => ServiceLocator.RegisterFactory<IObservable<float>>(null));
    }
    [Test]
    public void RegisterWithNotInterfaceType()
    {
        Assert.Throws<InvalidCastException>(() => ServiceLocator.Register<TestClass>(new TestClass()));
        Assert.Throws<InvalidCastException>(() => ServiceLocator.RegisterFactory<TestClass>(() => { return new TestClass(); }));
    }
    [Test]
    public void RegisterServiceWhenThisTypeAlreadyRegisteredAsService()
    {
        ServiceLocator.Register<IObservable<float>>(new TestClass());
        Assert.Throws<ArgumentException>(() => ServiceLocator.Register<IObservable<float>>(new TestClass()));
        Assert.IsFalse(ServiceLocator.TryRegister<IObservable<float>>(new TestClass()));
    }
    [Test]
    public void RegisterServiceWhenThisTypeAlreadyRegisteredAsFactory()
    {
        ServiceLocator.RegisterFactory<IObservable<float>>(() => new TestClass());
        Assert.Throws<ArgumentException>(() => ServiceLocator.Register<IObservable<float>>(new TestClass()));
        Assert.IsFalse(ServiceLocator.TryRegister<IObservable<float>>(new TestClass()));
    }
    [Test]
    public void RegisterFactoryWhenThisTypeAlreadyRegisteredAsService()
    {
        ServiceLocator.RegisterFactory<IObservable<float>>(() => new TestClass());
        Assert.Throws<ArgumentException>(() => ServiceLocator.RegisterFactory<IObservable<float>>(() => new TestClass()));
        Assert.IsFalse(ServiceLocator.TryRegisterFactory<IObservable<float>>(() => new TestClass()));
    }
    [Test]
    public void RegisterFactoryWhenThisTypeAlreadyRegisteredAsFactory()
    {
        ServiceLocator.Register<IObservable<float>>(new TestClass());
        Assert.Throws<ArgumentException>(() => ServiceLocator.RegisterFactory<IObservable<float>>(() => new TestClass()));
        Assert.IsFalse(ServiceLocator.TryRegisterFactory<IObservable<float>>(() => new TestClass()));
    }
    #endregion
    #region Get
    [Test]
    public void GetService()
    {
        var t = new TestClass();
        ServiceLocator.Register<IObservable<float>>(t);
        ServiceLocator.Register<ITestInterface>(t);
        var p = ServiceLocator.Get<IObservable<float>>();

        Assert.AreEqual(t, p);
    }
    [Test]
    public void GetFactory()
    {
        var t = new TestClass();
        ServiceLocator.RegisterFactory<IObservable<float>>(() => t);
        ServiceLocator.Register<ITestInterface>(t);
        var p = ServiceLocator.Get<IObservable<float>>();

        Assert.AreEqual(t, p);
    }

    [Test]
    public void GetUnregisteredType()
    {
        Assert.Throws<KeyNotFoundException>(() => ServiceLocator.Get<ITestInterface>());
        Assert.IsFalse(ServiceLocator.TryGet<ITestInterface>(out var t));
    }
    [Test]
    public void GetNotInterfaceType()
    {
        Assert.Throws<InvalidCastException>(() => ServiceLocator.Get<TestClass>());
        Assert.Throws<InvalidCastException>(() => ServiceLocator.TryGet<TestClass>(out var t));
    }

    #endregion
    #region Unregister
    [Test]
    public void UnregisterTypeService()
    {
        ServiceLocator.Register<ITestInterface>(new TestClass());
        Assert.IsTrue(ServiceLocator.IsTypeRegistered<ITestInterface>());
        Assert.IsTrue(ServiceLocator.TryUnregisterType<ITestInterface>());
        Assert.IsFalse(ServiceLocator.IsTypeRegistered<ITestInterface>());
    }
    [Test]
    public void UnregisterTypeFactory()
    {
        ServiceLocator.RegisterFactory<ITestInterface>(() => new TestClass());
        Assert.IsTrue(ServiceLocator.IsTypeRegistered<ITestInterface>());
        Assert.IsTrue(ServiceLocator.TryUnregisterType<ITestInterface>());
        Assert.IsFalse(ServiceLocator.IsTypeRegistered<ITestInterface>());
    }
    #endregion
    #region ReadRegistered
    [Test]
    public void CheckRegistered()
    {
        TestClass testClass = new TestClass();
        TestClass testClass2 = new TestClass();
        TestClass testClassFactory = new TestClass();


        ServiceLocator.Register<ITestInterface>(testClass);
        ServiceLocator.Register<ITestInterface2>(testClass2);
        ServiceLocator.RegisterFactory<IObservable<float>>(() => testClassFactory);

        Assert.IsTrue(ServiceLocator.GetServicesEnumerator().Count() == 2);
        Assert.AreEqual(testClass, ServiceLocator.Get<ITestInterface>());

        Assert.IsTrue(ServiceLocator.GetFactoriesEnumerator().Count() == 1);
        Assert.AreEqual(testClassFactory, ServiceLocator.Get<IObservable<float>>());

        Assert.AreNotEqual(testClass2, ServiceLocator.Get<ITestInterface>());
    }
    #endregion
    #region Set
    [Test]
    public void SetServiceWhenServiceWasInited()
    {
        TestClass testClass = new TestClass();
        TestClass testClass2 = new TestClass();
        ServiceLocator.Register<ITestInterface>(testClass);
        ServiceLocator.Set<ITestInterface>(testClass2);
        Assert.AreEqual(testClass2, ServiceLocator.Get<ITestInterface>());
    }
    [Test]
    public void SetServiceWhenFactoryWasInited()
    {
        TestClass testClass = new TestClass();
        TestClass testClass2 = new TestClass();
        ServiceLocator.RegisterFactory<ITestInterface>(() => testClass);
        ServiceLocator.Set<ITestInterface>(testClass2);
        Assert.AreEqual(testClass2, ServiceLocator.Get<ITestInterface>());
    }
    [Test]
    public void SetFactoryWhenServiceWasInited()
    {
        TestClass testClass = new TestClass();
        TestClass testClass2 = new TestClass();
        ServiceLocator.Register<ITestInterface>(testClass);
        ServiceLocator.SetFactory<ITestInterface>(()=>testClass2);
        Assert.AreEqual(testClass2, ServiceLocator.Get<ITestInterface>());
    }
    [Test]
    public void SetFactoryWhenFactoryWasInited()
    {
        TestClass testClass = new TestClass();
        TestClass testClass2 = new TestClass();
        ServiceLocator.RegisterFactory<ITestInterface>(() => testClass);
        ServiceLocator.SetFactory<ITestInterface>(()=>testClass2);
        Assert.AreEqual(testClass2, ServiceLocator.Get<ITestInterface>());
    }
    #endregion
}