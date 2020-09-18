using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Assets.Scripts.Manager
{
    public class AudioManager : BaseManager
    {
        private ReactiveProperty<float> bgmVolume = new ReactiveProperty<float>(1.0f);
        public IReadOnlyReactiveProperty<float> BGMVolume { get { return bgmVolume; } }
        private AudioSource BGM;

        protected override void OnInitializeManager()
        {
            BGM = GetComponent<AudioSource>();

            Main.CurrentGameState
                .Where(state => state == GameState.Initialize)
                .Subscribe(_ =>
                {
                    OnInitializeAudio();
                });

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    ClampBgmVolume();
                });
        }

        /// <summary>
        /// オーディオ関係の初期化
        /// </summary>
        /// 

        private void OnInitializeAudio()
        {
            BGM.Play(0);
        }

        /// <summary>
        /// BGMの音量を設定する
        /// </summary>
        /// <param name="value">BGMの音量(float:0-1)</param>
        public void SetBgmVolume(float value)
        {
            bgmVolume.Value += value;
            BGM.volume += value;
        }

        private void ClampBgmVolume()
        {
            var volume = this.bgmVolume.Value;
            volume = Mathf.Clamp(volume, 0.0f, 1.0f);
            this.bgmVolume.Value = volume;
        }
    }
}