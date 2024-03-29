﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    [RequireComponent(typeof(StageManager))]
    [RequireComponent(typeof(AudioManager))]
    public class MainGameManager : MonoBehaviour
    {
        // 現在のゲームの状態
        private ReactiveProperty<GameState> currentGameState = new ReactiveProperty<GameState>(GameState.Initialize);
        public IReadOnlyReactiveProperty<GameState> CurrentGameState { get { return currentGameState; } }

        // ポーズ中かどうか
        private ReactiveProperty<bool> isPause = new ReactiveProperty<bool>(false);
        public IReactiveProperty<bool> IsPause { get { return isPause; } }

        private void Awake()
        {
            //currentGameState.Subscribe(state => Debug.Log(state));
        }

        /// <summary>
        /// ゲームの状態を設定
        /// </summary>
        /// <param name="state">設定するゲームの状態</param>
        public void SetGameState(GameState state)
        {
            currentGameState.Value = state;
        }

        /// <summary>
        /// ポーズ中かどうかを設定する
        /// </summary>
        /// <param name="isPause">ポーズ中かどうか</param>
        public void SetIsPause(bool isPause)
        {
            this.isPause.Value = isPause;
        }
    }
}
