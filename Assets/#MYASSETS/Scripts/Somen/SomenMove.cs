﻿using System.Collections;
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
        public bool IsGrabbed { get { return isGrabbed; } }
        private bool isPause = false;
        public bool IsPause { get { return isPause; } }
        private const float LIMIT_MOVE_RIGHT = 1.5f;    // 右側の移動限界位置
        private const float LIMIT_MOVE_LEFT = -1.5f;    // 左側の移動限界位置
        private Coroutine startGameRoutine = null;
        private Coroutine grabbedRoutine = null;
        private Coroutine gravityRoutine = null;
        private SomenSound somenSound;
        protected override void OnInitialize()
        {
            somenSound = GetComponent<SomenSound>();

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
                .Where(_ => core.IsAlive.Value && !isGrabbed && !isPause)
                .Where(_ => core.CurrentGameState == GameState.Main)
                .Subscribe(moveDirection =>
                {
                    var direction = (transform.right * moveDirection.x);
                    Move(direction * moveSpeed);
                    Stop();
                });

            // ソーメンが移動可能範囲外に出ないかを監視
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    ClampSomenPosition();
                });

            // 箸に当たったとき
            this.OnTriggerEnterAsObservable()
                .Subscribe(col =>
                {
                    if (col.gameObject.tag == "ChopStick")
                    {
                        var i = col.transform.parent.gameObject
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
                                somenSound.ShotSE();
                                grabbedRoutine = StartCoroutine(GrabbedCoroutine(i));
                            }
                        }
                    }
                    else if (col.gameObject.tag == "Buket")
                    {
                        var j = col.GetComponent<Buckt>();
                        if (j != null)
                        {
                            if (gravityRoutine == null)
                            {
                                gravityRoutine = StartCoroutine(SwitchGravity(j));
                            }
                        }
                    }
                });
        }

        private IEnumerator SwitchGravity(Buckt buckt)
        {
            somenRigidBody.useGravity = true;
            yield return new WaitForSeconds(1.5f);
            somenRigidBody.useGravity = false;
            Stop();
            gravityRoutine = null;
            buckt.SetIsClear(true);
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
            somenRigidBody.angularVelocity = Vector3.zero;
        }

        /// <summary>
        /// 移動範囲を収める
        /// </summary>
        private void ClampSomenPosition()
        {
            var somenPosition = this.transform.position;
            somenPosition.x = Mathf.Clamp(somenPosition.x, LIMIT_MOVE_LEFT, LIMIT_MOVE_RIGHT);
            this.transform.position = new Vector3(somenPosition.x, somenPosition.y, somenPosition.z);
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
            const int TARGET_SHAKE_COUNT = 20;  // シェイクしなければいけない数
            int shakeCount = 0;                 // シェイクした数
            const float TIME_LIMIT = 3.0f;             // 残り時間
            float elapsedTime = Time.deltaTime;   // 経過時間
            Vector3 prevAcceleration = Vector3.zero;    // 1フレーム前の角速度

            while (elapsedTime < TIME_LIMIT)
            {
                elapsedTime += Time.deltaTime;
                if (CheckShake(InputEvent.MoveDirection.Value, prevAcceleration) == true)
                {
                    shakeCount++;
                }
                prevAcceleration = InputEvent.MoveDirection.Value;

                // 規定回数のカウントを超えれば抜け出す
                if (TARGET_SHAKE_COUNT < shakeCount)
                {
                    this.transform.position = tmpSomenPosition;
                    grabbedRoutine = null;
                    chopStick.SwitchOffIsGrab();
                    isGrabbed = chopStick.IsGrab.Value;
                    yield break;
                }
                yield return null;
            }
            // ここまで来たらゲームオーバー
            grabbedRoutine = null;
            core.SetIsAlive(false);
            isGrabbed = false;
            isPause = false;
        }

        /// <summary>
        /// シェイクしたか判定する
        /// </summary>
        /// <param name="acceleration">今回のフレームの角速度</param>
        /// <param name="prevAcceleration">1フレーム前の角速度</param>
        /// <returns>シェイクしたかどうか</returns>
        private bool CheckShake(Vector3 acceleration, Vector3 prevAcceleration)
        {
            var dot = Vector3.Dot(acceleration, prevAcceleration);
            if (dot < 0)
            {
                return true;
            }
            else { return false; }
        }

        public void SetIsGrabbed(bool isGrabbed)
        {
            this.isGrabbed = isGrabbed;
        }

        public void SetIsPause(bool isPause)
        {
            this.isPause = isPause;
        }
    }
}
