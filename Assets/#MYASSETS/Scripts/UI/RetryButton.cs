using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    private GameOverModalManagerPresenter modalPresenter;
    private void Start()
    {
        modalPresenter = GetComponentInParent<GameOverModalManagerPresenter>();
    }

    public void OnClick()
    {
        modalPresenter.SetCurrentGameState(GameState.Initialize);
    }
}
