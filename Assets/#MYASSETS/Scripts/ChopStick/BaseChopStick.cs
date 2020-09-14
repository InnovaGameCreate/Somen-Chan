using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.ChopStick
{
    public abstract class BaseChopStick : MonoBehaviour
    {
        // 箸が壊れているかどうかのイベント
        private ReactiveProperty<bool> isBroken = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsBroken { get { return isBroken; } }

        // ソーメンを掴んでいるかどうかのイベント
        private ReactiveProperty<bool> isGrab = new ReactiveProperty<bool>(false);
        public IReadOnlyReactiveProperty<bool> IsGrab { get { return isGrab; } }

        private ChopStickProvider chopStickProvider;

        private void Awake()
        {
            chopStickProvider = GetComponentInParent<ChopStickProvider>();
            OnInitializeChopStick();
        }

        /// <summary>
        /// 掴むフラグをオンに
        /// </summary>
        public void SwitchOnIsGrab()
        {
            isGrab.Value = true;
            chopStickProvider.SetIsGrab(isGrab.Value);
        }

        protected abstract void OnInitializeChopStick();
    }
}