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
        AudioSource BGM;

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
        /// 
        
        private void OnInitializeAudio()
        {
            BGM = GetComponent<AudioSource>();
            BGM.Play(0);
        }

        /// <summary>
        /// BGMの音量を設定する
        /// </summary>
        /// <param name="value">BGMの音量(float:0-1)</param>
        public void SetBgmVolume(float value)
        {
            bgmVolume = value;
        }

    }
}