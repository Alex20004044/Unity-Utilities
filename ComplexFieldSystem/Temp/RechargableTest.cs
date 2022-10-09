using MSFD;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargableTest : MonoBehaviour
{
    [SerializeField]
    Rechargable rechargable = new Rechargable(0,0,60,1);
    //[SerializeField]
    Vector2 vector2;
    void Start()
    {
        rechargable.StartRecharge();
    }
    [Button]
    void AddRechargableMod()
    {
        rechargable.GetRechargeSpeed().AddMod((x) => x * 10);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
