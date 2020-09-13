using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    public class StageManager : BaseManager
    {
        // void Start()のようなもの
        protected override void OnInitializeManager()
        {
            // ゲームの状態がInitialize(初期化)のとき実行される
            Main.CurrentGameState
                .Where(state => state==GameState.Initialize)
                .Subscribe(_ => {
                    OnInitializeStageObject();
                });
        }

        // TODO ここでオブジェクト（橋や竹）の配置を決定したい
        /// <summary>
        /// 箸のランダム配置やステージ位置の初期化などを行う
        /// </summary>
        private void OnInitializeStageObject()
        {
        }
    }
}