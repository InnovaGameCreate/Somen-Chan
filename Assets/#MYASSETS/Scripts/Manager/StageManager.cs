using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    public class StageManager : BaseManager
    {
        [SerializeField]
        private GameObject bamboo = default;
        [SerializeField]
        private GameObject chopsticks = default;
        [SerializeField]
        private int createNum = default;                    //箸と竹を生成する数
        [SerializeField]
        private Vector3 createPos = default;                //箸と竹を生成する場所
        private const float BAMBOO_SPAWN_MERGEN = 13.8f;    //竹を生成する間隔
        private const float CHOPSTICK_SPAWN_MERGEN = 6.0f;  //箸を生成する間隔
        private const float RIGHT_RANGE = 1.8f;             //右端までの長さ
        private const float LEFT_RANGE = -1.8f;             //左端までの長さ
        private const float CENTER = 0.0f;                  //中央
        private const float RAMDOM = 2.0f;                  //箸の上下のブレ

        // void Start()のようなもの
        protected override void OnInitializeManager()
        {
            // ゲームの状態がInitialize(初期化)のとき実行される
            Main.CurrentGameState
                .Where(state => state == GameState.Initialize)
                .Subscribe(_ =>
                {
                    OnInitializeStageObject();
                });
        }



        // TODO ここでオブジェクト（橋や竹）の配置を決定したい
        /// <summary>
        /// 箸のランダム配置やステージ位置の初期化などを行う
        /// </summary>
        private void OnInitializeStageObject()
        {
            var prob = 0.5f;
            Debug.Log("竹を配置");
            for (int i = 0; i < createNum; i++)
            {
                Instantiate(bamboo, createPos + new Vector3(0, i + BAMBOO_SPAWN_MERGEN * i, 0), Quaternion.Euler(-90.0f, 0.0f, 0.0f));
            }
            for (int i = 0; i < createNum * 2; i++)
            {
                Instantiate(chopsticks, createPos + new Vector3(Random.Range(CENTER, RIGHT_RANGE), i + CHOPSTICK_SPAWN_MERGEN * i + Random.Range(-RAMDOM, RAMDOM), 0), Quaternion.identity);
                if (spawnProbability(prob) == true)
                {
                    Instantiate(chopsticks, createPos + new Vector3(Random.Range(LEFT_RANGE, CENTER), i + CHOPSTICK_SPAWN_MERGEN * i + Random.Range(-RAMDOM, RAMDOM), 0), Quaternion.Euler(0.0f, 180.0f, 0.0f));
                }
            }

        }

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
    }
}