using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class MenuOpenButton : MonoBehaviour
{
    [SerializeField]
    private ModalManagerPresenter modalPresenter = default;

    public void OnClick()
    {
        modalPresenter.SetIsPause(true);
    }
}
