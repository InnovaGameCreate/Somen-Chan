using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Somen.Inputs
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
    }
}
