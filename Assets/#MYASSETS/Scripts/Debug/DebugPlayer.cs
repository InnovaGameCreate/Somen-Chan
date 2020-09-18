using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Somen.Inputs
{
    public class DebugPlayer : MonoBehaviour
    {
        private Rigidbody rb;
        private InputAndroid inputAndroid;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            inputAndroid = GetComponent<InputAndroid>();

            this.FixedUpdateAsObservable()
                .Subscribe(_ =>
                {
                    rb.AddForce((transform.right * inputAndroid.MoveDirection.Value.x) * 10.0f, ForceMode.Impulse);
                    rb.velocity = Vector3.zero;
                });
        }
    }
}