using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.Somen;

namespace Assets.Scripts.Somen.Inputs
{
    public class InputAndroid : BaseSomen, IInputEventProvider
    {
        [SerializeField]
        private Text accelerationX = default;
        [SerializeField]
        private Text accelerationY = default;
        [SerializeField]
        private Text accelerationZ = default;

        private ReactiveProperty<Vector3> moveDirection = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> MoveDirection { get { return moveDirection; } }

        protected override void OnInitialize()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.acceleration)
                .Subscribe(accelerationVector =>
                {
                    moveDirection.Value = accelerationVector;
                    accelerationX.text = "ジャイロX:" + accelerationVector.x;
                    accelerationY.text = "ジャイロY:" + accelerationVector.y;
                    accelerationZ.text = "ジャイロZ:" + accelerationVector.z;
                });
        }
    }
}