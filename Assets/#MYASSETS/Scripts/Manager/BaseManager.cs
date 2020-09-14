using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public abstract class BaseManager : MonoBehaviour
    {
        protected MainGameManager Main;
        private void Awake()
        {
            Main = GetComponent<MainGameManager>();
            OnInitializeManager();

        }

        // 下位クラスのManagerの初期化
        protected abstract void OnInitializeManager();
    }
}