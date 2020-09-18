using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.ChopStick
{
    public class ChopStickProvider : MonoBehaviour
    {
        private ReactiveProperty<bool> isGrab = new ReactiveProperty<bool>();
        public IReadOnlyReactiveProperty<bool> IsGrab { get { return isGrab; } }

        public void SetIsGrab(bool isGrab)
        {
            this.isGrab.Value = isGrab;
        }
    }
}