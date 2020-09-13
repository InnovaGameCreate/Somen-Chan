using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.Somen.Inputs;

namespace Assets.Scripts.Somen
{
    public abstract class BaseSomen : MonoBehaviour
    {
        protected SomenCore core;
        protected Rigidbody somenRigidBody;
        protected IInputEventProvider InputEvent;
        private void Awake()
        {
            core = GetComponent<SomenCore>();
            somenRigidBody = GetComponent<Rigidbody>();
            InputEvent = GetComponent<IInputEventProvider>();
            OnInitialize();
        }
        protected abstract void OnInitialize();
    }
}