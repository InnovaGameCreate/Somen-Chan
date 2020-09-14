using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.ChopStick
{
    public class ChopStickAnimationController : BaseChopStick
    {
        protected override void OnInitializeChopStick()
        {
            Debug.Log("箸初期化");
            // ソーメンを掴んだとき
            IsGrab
                .Where(isGrab => isGrab)
                .Subscribe(_ =>
                {
                    Debug.Log("衝突");
                });

            // 箸が壊れたとき
            IsBroken
                .Where(isBroken => isBroken)
                .Subscribe(_ =>
                {
                });
        }
    }
}