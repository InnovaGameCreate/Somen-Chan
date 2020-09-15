using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.ChopStick;

namespace Assets.Scripts.Somen
{
    public class SomenMove : BaseSomen
    {
        private const float moveSpeed = 10.0f;           // 横方向の移動速度
        private const float accelationYThreshold = 0.0f; // ゲームスタートのジャイロセンサの閾値
        private Vector3 tmpSomenPosition;
        private bool isGrabbed = false;
        private Coroutine startGameRoutine = null;
        private Coroutine grabbedRoutine = null;
        protected override void OnInitialize()
        {
            // ゲーム開始時の動き
            InputEvent.MoveDirection
                .Where(_ => !core.IsAlive.Value)
                .Where(_ => core.CurrentGameState == GameState.Initialize)
                .Subscribe(moveDirection =>
                {
                    if (startGameRoutine == null)
                    {
                        startGameRoutine = StartCoroutine(StartGameCoroutine());
                    }
                });

            // 左右方向の移動
            InputEvent.MoveDirection
                .Where(_ => core.IsAlive.Value && !isGrabbed)
                .Where(_ => core.CurrentGameState == GameState.Main)
                .Subscribe(moveDirection =>
                {
                    var direction = (transform.right * moveDirection.x);
                    Move(direction * moveSpeed);
                    Stop();
                });

            // 箸に当たったとき
            this.OnTriggerEnterAsObservable()
                .Subscribe(chopStick =>
                {
                    var i = chopStick.transform.parent.gameObject
                                     .transform.parent.gameObject
                                     .transform.parent.gameObject
                                     .transform.parent.gameObject
                                     .transform.parent.gameObject
                                     .GetComponent<BaseChopStick>();
                    if (i != null)
                    {
                        i.SwitchOnIsGrab();                         // 掴まれたフラグをオンに
                        isGrabbed = i.IsGrab.Value;
                        Stop();                                     // プレイヤーを止まらせる
                        var chopstickPosition = i.GrabSomen();      // 箸のコライダーを消す&掴まれる
                        tmpSomenPosition = transform.position;
                        this.transform.position = new Vector3(chopstickPosition.x, chopstickPosition.y, transform.position.z);
                        if (grabbedRoutine == null)
                        {
                            grabbedRoutine = StartCoroutine(GrabbedCoroutine(i));
                        }
                    }
                });
        }

        /// <summary>
        /// 移動
        /// </summary>
        /// <param name="direction">移動方向</param>
        private void Move(Vector3 direction)
        {
            somenRigidBody.AddForce(direction, ForceMode.Impulse);
        }

        /// <summary>
        /// RigidBodyの速度を0にする
        /// </summary>
        public void Stop()
        {
            somenRigidBody.velocity = Vector3.zero;
        }

        /// <summary>
        /// ゲームをスタートさせるためのコルーチン
        /// </summary>
        private IEnumerator StartGameCoroutine()
        {
            float time = 3.0f;                  // 計測時間
            float startForce = 50.0f;          // スタート時に加える力
            var elapsedTime = Time.deltaTime;   // 経過時間
            while (true)
            {
                // ジャイロの傾きが十分でないなら経過時間初期化
                if (InputEvent.MoveDirection.Value.y < accelationYThreshold)
                {
                    elapsedTime = Time.deltaTime;
                }
                elapsedTime += Time.deltaTime;  // 経過時間を加算

                if (time < elapsedTime)
                {
                    core.SetIsAlive(true);   // isAliveのフラグ切り替え
                    Move(transform.forward * startForce);   // 前方方向にスタート時に加える
                    Stop();
                    startGameRoutine = null;
                    yield break;
                }
                yield return null;
            }
        }

        /// <summary>
        /// 捕まったときのコルーチン
        /// </summary>
        /// <param name="chopStick">箸のインスタンス</param>
        private IEnumerator GrabbedCoroutine(BaseChopStick chopStick)
        {
            yield return new WaitForSeconds(3.0f);
            this.transform.position = tmpSomenPosition;
            grabbedRoutine = null;
            chopStick.SwitchOffIsGrab();
            isGrabbed = chopStick.IsGrab.Value;
        }
    }
}
