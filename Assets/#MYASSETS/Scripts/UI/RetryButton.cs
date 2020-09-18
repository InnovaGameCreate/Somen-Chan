using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    private GameClearModalManagerPresenter modalPresenter;
    private void Start()
    {
        modalPresenter = GetComponentInParent<GameClearModalManagerPresenter>();
    }

    public void OnClick()
    {
        modalPresenter.SetCurrentGameState(GameState.Initialize);
    }
}
