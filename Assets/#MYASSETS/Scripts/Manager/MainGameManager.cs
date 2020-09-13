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

        private StageManager stageManager;

        private void Awake()
        {
            stageManager = GetComponent<StageManager>();
        }

        /// <summary>
        /// ゲームの状態を設定
        /// </summary>
        /// <param name="state">設定するゲームの状態</param>
        public void SetGamaState(GameState state)
        {
            currentGameState.Value = state;
        }
    }
}
