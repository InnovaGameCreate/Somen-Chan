using Assets.Scripts.Somen;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Buckt : MonoBehaviour
{
    private ReactiveProperty<bool> isClear = new ReactiveProperty<bool>(false);
    public IReadOnlyReactiveProperty<bool> IsClear { get { return isClear; } }
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = this.transform.position;
    }

    public void SetIsClear(bool isClear)
    {
        this.isClear.Value = isClear;
    }

    public void ResetBuketPosition()
    {
        this.transform.position = startPosition;
    }
}
