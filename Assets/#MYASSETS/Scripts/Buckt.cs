using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.Somen;

public class Buckt : MonoBehaviour
{
    private void Start()
    {
        this.OnTriggerEnterAsObservable()
            .Subscribe(somen =>
            {
                var i = somen.GetComponent<SomenCore>();
                if (i != null)
                {

                }
            });
    }
}
