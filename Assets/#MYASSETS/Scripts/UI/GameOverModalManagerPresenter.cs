using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Assets.Scripts.Manager;

public class GameOverModalManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private MainGameManager mainGameManager = default;
    private GameOverModalView gameOverModalView;

    private void Start()
    {
        gameOverModalView = GetComponent<GameOverModalView>();

        mainGameManager.CurrentGameState
            .Subscribe(state =>
            {
                if (state==GameState.GameOver)
                {
                    gameOverModalView.ShowModal();
                }
                else
                {
                    gameOverModalView.CloseModal();
                }
            });
    }

    public void SetCurrentGameState(GameState state)
    {
        mainGameManager.SetGameState(state);
    }
}
