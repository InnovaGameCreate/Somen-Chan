using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    [RequireComponent(typeof(StageManager))]
    public class MainGameManager : MonoBehaviour
    {
        // 現在のゲームの状態
        private ReactiveProperty<GameState> currentGameState = new ReactiveProperty<GameState>(GameState.Initialize);
        public IReadOnlyReactiveProperty<GameState> CurrentGameState { get { return currentGameState; } }

        // ポーズ中かどうか
        private ReactiveProperty<bool> isPause = new ReactiveProperty<bool>(false);
        public IReactiveProperty<bool> IsPause { get { return isPause; } }

        private StageManager stageManager;

        private void Awake()
        {
            stageManager = GetComponent<StageManager>();
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
