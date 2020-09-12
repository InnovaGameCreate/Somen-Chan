using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class InputAndroid : MonoBehaviour
{
    [SerializeField]
    private Text accelerationX = default;
    [SerializeField]
    private Text accelerationY = default;
    [SerializeField]
    private Text accelerationZ = default;

    private ReactiveProperty<Vector3> acceleration = new ReactiveProperty<Vector3>();
    public IReadOnlyReactiveProperty<Vector3> Acceleration{get{return acceleration;}}
    void Start()
    {
        this.UpdateAsObservable()
            .Select(_ => Input.acceleration)
            .Subscribe(accelerationVector => {
                acceleration.Value = accelerationVector;
                accelerationX.text = "ジャイロX:" + accelerationVector.x;
                accelerationY.text = "ジャイロY:" + accelerationVector.y;
                accelerationZ.text = "ジャイロZ:" + accelerationVector.z;
            });
    }
}
