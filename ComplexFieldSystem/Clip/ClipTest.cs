using MSFD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class ClipTest : MonoBehaviour
{
    [SerializeField]
    Clip clip = new Clip();
    void Start()
    {
        clip.StartRecharge();
        clip.GetObsOnCanShoot().Subscribe((_) => Debug.Log("OnCanShoot"));
        clip.GetObsOnShoot().Subscribe((_) => Debug.Log("Shoot!"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
