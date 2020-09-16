using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using Assets.Scripts.Manager;
public class SoundAudioPresenter : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager = default;
    private SoundImage soundImage;

    private void Start()
    {
        soundImage = GetComponent<SoundImage>();

        soundImage.DragDirection
            .SkipLatestValueOnSubscribe()
            .Select(direction => Mathf.Sign(direction) * 0.03f)
            .Subscribe(volume => audioManager.SetBgmVolume(volume));

        audioManager.BGMVolume
            .ThrottleFirst(TimeSpan.FromSeconds(0.1f))
            .Subscribe(volume =>
            {
                soundImage.ControlFillArea(volume);
            });
    }
}
