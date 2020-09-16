using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class SoundImage : MonoBehaviour
{
    private ReactiveProperty<float> dragDirection = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> DragDirection { get { return dragDirection; } }
    private Vector3 prevTouchPosition;
    private Image soundImage;
    private void Awake()
    {
        soundImage = GetComponent<Image>();
    }

    public void PointerDown()
    {
        prevTouchPosition = Input.mousePosition;
    }

    public void Drag()
    {
        var touchPosition = Input.mousePosition;
        dragDirection.Value = touchPosition.y - prevTouchPosition.y;
        prevTouchPosition = touchPosition;
    }

    public void ControlFillArea(float value)
    {
        soundImage.fillAmount = value;
    }
}
