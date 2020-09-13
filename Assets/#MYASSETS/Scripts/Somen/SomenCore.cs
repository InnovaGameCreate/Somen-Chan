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

        private void Awake()
        {

        }

        /// <summary>
        /// isAliveのフラグ切り替えメソッド
        /// </summary>
        /// <param name="value">切り替え先</param>
        public void SwitchIsAlive(bool value){
            isAlive.Value = value;
        }
    }
}