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
        private ReactiveProperty<Vector3> moveDirection = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> MoveDirection { get { return moveDirection; } }

        protected override void OnInitialize()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.acceleration)
                .Subscribe(accelerationVector =>
                {
                    moveDirection.Value = accelerationVector;
                });
        }
    }
}