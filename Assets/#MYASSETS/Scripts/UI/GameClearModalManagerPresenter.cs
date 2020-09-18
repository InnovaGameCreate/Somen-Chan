using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Assets.Scripts.Manager;

public class GameClearModalManagerPresenter : MonoBehaviour
{
    [SerializeField]
    private MainGameManager mainGameManager = default;
    [SerializeField]
    private Buckt buckt = default;
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
                    buckt.ResetBuketPosition();
                }
                else
                {
                    GameClearModalView.CloseModal();
                    buckt.SetIsClear(false);
                }
            });

        buckt.IsClear
            .Where(isClear => isClear)
            .Subscribe(_ =>
            {
                SetCurrentGameState(GameState.GameClear);
            });
    }

    public void SetCurrentGameState(GameState state)
    {
        mainGameManager.SetGameState(state);
    }
}
