using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReStartButton : MonoBehaviour
{
    private ModalManagerPresenter modalManager;

    private void Start()
    {
        modalManager = GetComponentInParent<ModalManagerPresenter>();
    }

    public void OnClick()
    {
        modalManager.SetCurrentGameState(GameState.Initialize);
        modalManager.SetIsPause(false);
    }
}
