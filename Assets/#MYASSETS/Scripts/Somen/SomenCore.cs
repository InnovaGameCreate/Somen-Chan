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
        private Vector3 startSomenPositin;

        private void Awake()
        {
            currentGameState = GameState.Initialize;
            startSomenPositin = this.transform.position;
        }

        /// <summary>
        /// ソーメンの初期位置に移動
        /// </summary>
        public void ResetSomenPosition()
        {
            this.transform.position = startSomenPositin;
        }

        /// <summary>
        /// isAliveのフラグ切り替えメソッド
        /// </summary>
        /// <param name="value">切り替え先</param>
        public void SetIsAlive(bool value)
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