using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private ModalManagerPresenter modalPresenter;
    private void Start()
    {
        modalPresenter = GetComponentInParent<ModalManagerPresenter>();
    }

    public void OnClick()
    {
        modalPresenter.SetIsPause(false);
    }
}
