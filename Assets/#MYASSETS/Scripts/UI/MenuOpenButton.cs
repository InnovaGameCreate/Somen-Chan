using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.ChopStick;

public class MenuOpenButton : MonoBehaviour
{
    [SerializeField]
    private ModalManagerPresenter modalPresenter = default;
    [SerializeField]
    private ChopStickProvider chopStickProvider = default;
    private Button menuOpenButton;

    private void Start()
    {
        menuOpenButton = GetComponent<Button>();

        // ソーメンが掴まれると押せなくなる
        chopStickProvider.IsGrab
            .Subscribe(isGrab =>
            {
                if (isGrab)
                {
                    SetButtonInteractable(false);
                }
                else { SetButtonInteractable(true); }
            });
    }

    public void OnClick()
    {
        modalPresenter.SetIsPause(true);
    }

    /// <summary>
    /// ボタンを押せるかどうかをセットする
    /// </summary>
    /// <param name="isInteractable">ボタンを押せるかどうか</param>
    public void SetButtonInteractable(bool isInteractable)
    {
        menuOpenButton.interactable = isInteractable;
    }
}
