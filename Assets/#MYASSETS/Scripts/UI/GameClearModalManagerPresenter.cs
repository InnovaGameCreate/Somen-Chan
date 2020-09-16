using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Scripts.Manager;

public class GameClearModalManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private MainGameManager mainGameManager = default;
    private GameClearModalView GameClearModalView;

    private void Start()
    {
        GameClearModalView = GetComponent<GameClearModalView>();

        mainGameManager.CurrentGameState
            .Subscribe(state =>
            {
                if (state == GameState.GameClear)
                {
                    GameClearModalView.ShowModal();
                }
                else
                {
                    GameClearModalView.CloseModal();
                }
            });
    }

    public void SetCurrentGameState(GameState state)
    {
        mainGameManager.SetGameState(state);
    }
}
