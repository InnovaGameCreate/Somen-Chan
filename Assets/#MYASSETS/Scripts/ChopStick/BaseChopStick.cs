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

        [SerializeField]
        private Collider chopstickCollider = default;

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

        /// <summary>
        /// 掴むフラグをオフに
        /// </summary>
        public void SwitchOffIsGrab()
        {
            isGrab.Value = false;
            chopStickProvider.SetIsGrab(isGrab.Value);
        }

        /// <summary>
        /// ソーメンを掴む
        /// </summary>
        /// <returns>コライダーの位置を返す</returns>
        public Vector3 GrabSomen()
        {
            UnEnableCollider();
            return chopstickCollider.transform.position;
        }

        /// <summary>
        /// コライダーを外す
        /// </summary>
        public void UnEnableCollider()
        {
            chopstickCollider.enabled = false;
        }

        protected abstract void OnInitializeChopStick();
    }
}