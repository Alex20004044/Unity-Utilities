using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class ModFieldTest : MonoBehaviour
{
    [SerializeField]
    ModField<float> modField = new ModField<float>();
    float f = 10;

    [SerializeField]
    Delta delta = new Delta();
    void Start()
    {
        modField.Select((x)=>x*10).Subscribe((x) => Debug.Log(x)).AddTo(this);
        f = modField.BaseValue;

        delta.GetObsOnIncrease().Subscribe((x) => Debug.Log(x)).AddTo(this);
    }

}
