using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.ChopStick
{
    public class ChopStickAnimationController : BaseChopStick
    {
        private Animator animator;
        protected override void OnInitializeChopStick()
        {
            animator = GetComponent<Animator>();

            // ソーメンを掴んだとき
            IsGrab
                .Subscribe(isGrab =>
                {
                    if (isGrab)
                    {
                        animator.SetBool("IsGrab", true);
                    }
                    else
                    {
                        animator.SetBool("IsGrab", false);
                    }
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