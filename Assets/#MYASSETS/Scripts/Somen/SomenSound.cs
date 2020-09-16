using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Somen
{
    public class SomenSound : BaseSomen
    {
        private AudioSource audioSource;
        [SerializeField]
        private AudioClip grabbedSE = default;

        protected override void OnInitialize()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// 箸を掴まれたときのSEを鳴らす
        /// </summary>
        public void ShotSE()
        {
            audioSource.PlayOneShot(grabbedSE);
        }
    }
}