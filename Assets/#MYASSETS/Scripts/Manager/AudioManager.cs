using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    public class AudioManager : BaseManager
    {
        [SerializeField]
        private float bgmVolume = 0.5f; // BGMの音量
        [SerializeField]
        private float seVolume = 0.5f;  // SEの音量

        protected override void OnInitializeManager()
        {
            Main.CurrentGameState
                .Where(state => state == GameState.Initialize)
                .Subscribe(_ =>
                {
                    OnInitializeAudio();
                });
        }

        /// <summary>
        /// オーディオ関係の初期化
        /// </summary>
        private void OnInitializeAudio()
        {
        }

        /// <summary>
        /// BGMの音量を設定する
        /// </summary>
        /// <param name="value">BGMの音量(float:0-1)</param>
        public void SetBgmVolume(float value)
        {
            bgmVolume = value;
        }

        /// <summary>
        /// SEの音量を設定する
        /// </summary>
        /// <param name="value">SEの音量(float:0-1)</param>
        public void SetSeVolume(float value)
        {
            seVolume = value;
        }
    }
}