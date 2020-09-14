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
        private bool isGrab = false;    // そうめんが掴まれているかどうか
        protected bool IsGrab { get { return isGrab; } }

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
        /// そうめんが掴まれたどうかをセットする
        /// </summary>
        /// <param name="isGrab">そうめんが掴まれたどうかの状態</param>
        public void SetIsGrab(bool isGrab)
        {
            this.isGrab = isGrab;
        }
    }
}
