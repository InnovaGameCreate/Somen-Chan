using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.ChopStick;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Manager
{
    public class StageManager : BaseManager
    {
        [SerializeField]
        private Transform stageObject = default;
        private const float SCROLL_SPEED = 0.16f;
        private ChopStickProvider chopStickProvider;

        private float ElapsedTime { get; set; } = 0.0f;

        #region ステージ生成関係
        [SerializeField]
        private GameObject bamboo = default;
        [SerializeField]
        private GameObject chopsticks = default;
        [SerializeField]
        private int createNum = default;                    //箸と竹を生成する数
        private const float BAMBOO_SPAWN_MERGEN = 13.8f;    //竹を生成する間隔
        private const float CHOPSTICK_SPAWN_MERGEN = 6.0f;  //箸を生成する間隔
        private const float RIGHT_RANGE = 1.8f;             //右端までの長さ
        private const float LEFT_RANGE = -1.8f;             //左端までの長さ
        private const float CENTER = 0.0f;                  //中央
        private const float RAMDOM = 2.0f;                  //箸の上下のブレ
        #endregion

        protected override void OnInitializeManager()
        {
            chopStickProvider = stageObject.gameObject.GetComponent<ChopStickProvider>();

            // ゲームの状態がInitialize(初期化)のとき実行される
            Main.CurrentGameState
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case GameState.Initialize:
                            OnInitializeStageObject();
                            ElapsedTime = 0.0f;
                            break;
                        case GameState.GameOver:
                            chopStickProvider.SetIsGrab(false);
                            break;
                    }
                });
            
            // スクロールをする
            this.UpdateAsObservable()
                .Where(_ => Main.CurrentGameState.Value == GameState.Main && !Main.IsPause.Value)
                .Where(_ => !chopStickProvider.IsGrab.Value)
                .Subscribe(_ =>
                {
                    ElapsedTime += Time.deltaTime;
                    ScrollStage(SCROLL_SPEED);
                });
        }

        /// <summary>
        /// 箸のランダム配置やステージ位置の初期化などを行う
        /// </summary>
        private void OnInitializeStageObject()
        {
            DestroyStageObject();
            CreateStageObject();
        }

        /// <summary>
        /// 確率を求め，結果を返す
        /// </summary>
        /// <param name="prob">確率(float:0-1)</param>
        /// <returns></returns>
        private bool spawnProbability(float prob)
        {
            if (prob <= Random.Range(0.0f, 1.0f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ステージを自動生成する
        /// </summary>
        private void CreateStageObject()
        {
            var prob = 0.5f;
            for (int i = 0; i < createNum; i++)
            {
                Instantiate(bamboo, new Vector3(0, i + BAMBOO_SPAWN_MERGEN * i, 0), Quaternion.Euler(-90.0f, 0.0f, 0.0f), stageObject);
            }
            for (int j = 0; j < createNum * 2; j++)
            {
                Instantiate(chopsticks, new Vector3(Random.Range(CENTER, RIGHT_RANGE), j + CHOPSTICK_SPAWN_MERGEN * j + Random.Range(-RAMDOM, RAMDOM), -1.1f), Quaternion.identity, stageObject);
                if (spawnProbability(prob) == true)
                {
                    Instantiate(chopsticks, new Vector3(Random.Range(LEFT_RANGE, CENTER), j + CHOPSTICK_SPAWN_MERGEN * j + Random.Range(-RAMDOM, RAMDOM), -1.1f), Quaternion.Euler(0.0f, 180.0f, 0.0f), stageObject);
                }
            }
        }

        /// <summary>
        /// ステージオブジェクトをすべて削除
        private void DestroyStageObject()
        {
            foreach (Transform children in stageObject.transform)
            {
                if (children.gameObject.name != "Buket")
                {
                    Destroy(children.gameObject);
                }
            }
        }

        /// <summary>
        /// ステージをスクロールする
        /// </summary>
        /// <param name="scrollSpeed">スクロールのスピード</param>
        private void ScrollStage(float scrollSpeed)
        {
            var power = ElapsedTime >= 60 ? 1.5f : 1.0f;
            stageObject.transform.position -= new Vector3(0.0f, power * scrollSpeed, 0.0f);
        }
    }
}