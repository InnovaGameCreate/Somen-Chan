using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Somen
{
    public class SomenCore : MonoBehaviour
    {
        private ReactiveProperty<bool> isAlive = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> IsAlive { get { return isAlive; } }
        private GameState currentGameState;
        public GameState CurrentGameState { get { return currentGameState; } }

        private void Awake()
        {
            currentGameState = GameState.Initialize;
        }

        /// <summary>
        /// isAliveのフラグ切り替えメソッド
        /// </summary>
        /// <param name="value">切り替え先</param>
        public void SwitchIsAlive(bool value)
        {
            isAlive.Value = value;
        }

        /// <summary>
        /// ゲームの状態を設定
        /// </summary>
        /// <param name="state">設定するゲームの状態</param>
        public void SetGameState(GameState state)
        {
            currentGameState = state;
        }
    }
}